using Microsoft.AspNetCore.Mvc;
using MyShop.Data;
using MyShop.Models;
using System.Collections;
using System.Collections.Generic;

namespace MyShop.Controllers
{
    public class CatigoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CatigoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Catigory> objList = _db.Catigory;
            return View(objList);
        }
        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }
        //POST - CREATE, передаем ссылку на текущий объект Catigory который надо добавить в БД
        [HttpPost]//Явно указан что это пост-метод
        [ValidateAntiForgeryToken]//Что токен безопасен, надо во всех пост-методах
        public IActionResult Create(Catigory obj)
        {
            if (ModelState.IsValid)//Проверяет выполнены ли все правила для этой модели
            {
                _db.Catigory.Add(obj);//добавление в таблицу данных
                _db.SaveChanges();//сохранение изменений
                return RedirectToAction("Index");//редирект обратно к списку категорий
            }
            return View(obj);  

        }
        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Catigory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Catigory obj)
        {
            if (ModelState.IsValid)
            {
                _db.Catigory.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Catigory.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Catigory obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _db.Catigory.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}
