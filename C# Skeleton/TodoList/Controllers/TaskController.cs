﻿using System;
using System.Linq;
using System.Web.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    [ValidateInput(false)]
	public class TaskController : Controller
	{
	    [HttpGet]
        [Route("")]
	    public ActionResult Index()
	    {
            using (var db = new TodoListDbContext())
            {
                var tasks = db.Tasks.ToList();

                return View(tasks);
            }
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
		{
            return View();
		}

		[HttpPost]
		[Route("create")]
        [ValidateAntiForgeryToken]
		public ActionResult Create(Task task)
		{
            if (ModelState.IsValid)
            {
                var taskCreated = new Task{
                    Title = task.Title,
                    Comments = task.Comments
                    };

                using (var db = new TodoListDbContext())
                {
                    db.Tasks.Add(taskCreated);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(task);
        }

		[HttpGet]
		[Route("delete/{id}")]
        public ActionResult Delete(int id)
		{
            using (var db = new TodoListDbContext())
            {
                var task = db.Tasks
                    .FirstOrDefault(t => t.Id == id);

                if (task == null)
                {
                    return RedirectToAction("Index");
                }

                return View(task);
            }
        }

		[HttpPost]
		[Route("delete/{id}")]
        [ValidateAntiForgeryToken]
		public ActionResult DeleteConfirm(int id)
		{
            using (var db = new TodoListDbContext())
            {
                var task = db.Tasks
                    .FirstOrDefault(t => t.Id == id);

                if (task == null)
                {
                    return RedirectToAction("Index");
                }

                db.Tasks.Remove(task);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
	}
}