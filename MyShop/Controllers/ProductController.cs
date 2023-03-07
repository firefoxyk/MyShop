using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Data;
using MyShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product.Include(u => u.Catigory).Include(u=>u.Application);
            //IEnumerable<Product> objList = _db.Product;
            //foreach (var obj in objList)
            //{
            //    obj.Catigory = _db.Catigory.FirstOrDefault(u => u.CategoryId == obj.CategoryId);
            //};

            return View(objList);
        }


        //GET - UPSERT
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _db.Catigory.Select(i => new SelectListItem
            //{
            //    Text = i.CategoryName,
            //    Value = i.CategoryId.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;

            //Product product = new Product();

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CatigorySelectList = _db.Catigory.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                }),
                ApplicationSelectList = _db.Application.Select(i => new SelectListItem
                {
                    Text = i.ApplicationName,
                    Value = i.ApplicationId.ToString()
                })
            };
            if (id == null)
            {   //this is for create
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Product.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.ProductId == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;

                    _db.Product.Add(productVM.Product);//добавление в таблицу данных
                }
                else
                {
                    //updating
                    var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.ProductId == productVM.Product.ProductId);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _db.Product.Update(productVM.Product);
                }
                _db.SaveChanges();//сохранение изменений в БД
                return RedirectToAction("Index");
            }
            productVM.CatigorySelectList = _db.Catigory.Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
            productVM.ApplicationSelectList = _db.Application.Select(i => new SelectListItem
            {
                Text = i.ApplicationName,
                Value = i.ApplicationId.ToString()
            });
            return View(productVM);
        }



        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _db.Product.Include(u => u.Catigory).Include(u => u.Application).FirstOrDefault(u => u.ProductId == id);//Загрузка продукта из БД на основе id 

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Product.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);//obj.Image - там хранится имя файла

            var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.ProductId == id);

            if (System.IO.File.Exists(oldFile))
            {
                    System.IO.File.Delete(oldFile);
            }
            _db.Product.Remove(obj);
            _db.SaveChanges();//сохранение изменений в БД
            return RedirectToAction("Index");
        }
    }
}