using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jewels.DAL;
using Jewels.Models;
using PagedList;

namespace Jewels.Controllers
{
    public class ChatLieuSPsController : Controller
    {
        private JewelsContext db = new JewelsContext();

        // GET: ChatLieuSPs

        public ActionResult Index(int? page)
        {

            /* if (Session["user"] == null)
             {
                 return RedirectToAction("DangNhap", "User");
             }*/

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 8;
            var IsChatLieus = db.ChatLieuSPs.AsNoTracking()
                   .OrderByDescending(x => x.ChatLieuSPID);
            PagedList<ChatLieuSP> models = new PagedList<ChatLieuSP>(IsChatLieus, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;


            return View(models);
        }

        // GET: ChatLieuSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatLieuSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChatLieuSPID,TenChatLieuSP")] ChatLieuSP chatLieuSP)
        {
            if (ModelState.IsValid)
            {
                db.ChatLieuSPs.Add(chatLieuSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chatLieuSP);
        }

        // GET: ChatLieuSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieuSP chatLieuSP = db.ChatLieuSPs.Find(id);
            if (chatLieuSP == null)
            {
                return HttpNotFound();
            }
            return View(chatLieuSP);
        }

        // POST: ChatLieuSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatLieuSPID,TenChatLieuSP")] ChatLieuSP chatLieuSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatLieuSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chatLieuSP);
        }

        // GET: ChatLieuSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieuSP chatLieuSP = db.ChatLieuSPs.Find(id);
            if (chatLieuSP == null)
            {
                return HttpNotFound();
            }
            return View(chatLieuSP);
        }

        // POST: ChatLieuSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChatLieuSP chatLieuSP = db.ChatLieuSPs.Find(id);
            db.ChatLieuSPs.Remove(chatLieuSP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}