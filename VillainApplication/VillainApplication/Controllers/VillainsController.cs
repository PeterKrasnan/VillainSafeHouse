using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VillainApplication.Models;

namespace VillainApplication.Controllers
{
	public class VillainsController : Controller
	{
		private VillainsContext _context;
		public VillainsController()
		{
			_context = new VillainsContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}


		public ViewResult Index()
		{
			return View(_context.Villains);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Villain villain)
		{
			if (ModelState.IsValid)
			{
				_context.Villains.Add(villain);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(villain);
		}

		public ActionResult Details(int? id)
		{
			return Get(id);
		}


		public ActionResult Edit(int? id)
		{
			return Get(id);
		}

		[HttpPost]
		public ActionResult Edit(Villain villain)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Entry(villain).State = EntityState.Modified;
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
				return View(villain);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return View();
			}
		}

		[HttpPost]
		public ActionResult Delete(int? id, Villain vill)
		{
			try
			{
				Villain villain = new Villain();;

				if (ModelState.IsValid)
				{
					if (id == null)
					{
						return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
					}
					villain = _context.Villains.Find(id);

					if (villain == null)
					{
						return HttpNotFound();
					}

					_context.Villains.Remove(villain);
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
				return View(villain);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

	
		public ActionResult Delete(int? id)
		{
			return Get(id);
		}

		private ActionResult Get(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Villain villain = _context.Villains.Find(id);

			if (villain == null)
			{
				return HttpNotFound();
			}

			return View(villain);
		}


	}
}