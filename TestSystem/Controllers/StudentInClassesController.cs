using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    public class StudentInClassesController : ApiController
    {
        private HistoryContext db = new HistoryContext();

        // GET: api/StudentInClasses
        public IQueryable<StudentInClass> GetStudentsInClasses()
        {
            return db.StudentsInClasses;
        }

        // GET: api/StudentInClasses/5
        [ResponseType(typeof(StudentInClass))]
        public IHttpActionResult GetStudentInClass(int id)
        {
            StudentInClass studentInClass = db.StudentsInClasses.Find(id);
            if (studentInClass == null)
            {
                return NotFound();
            }

            return Ok(studentInClass);
        }

        // PUT: api/StudentInClasses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentInClass(int id, StudentInClass studentInClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentInClass.Id)
            {
                return BadRequest();
            }

            db.Entry(studentInClass).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentInClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentInClasses
        //Добавление ученика в класс
        [ResponseType(typeof(StudentInClass))]
        public IHttpActionResult PostStudentInClass(StudentInClass studentInClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentsInClasses.Add(studentInClass);
            studentInClass.Relevance = true; //Делаем отметку, что ученик сейчас учится в этом классе
            studentInClass.StartDate = DateTime.Now; //Записываем дату начала обучения
            //+ Автоматически должна ставиться планруемая дата окончания обучения в данном классе 
            //(не знаю, сейчас оставляют на второй год или нет)
            //Проставляем теоретическую дату окончания обучения в текущем классе
            studentInClass.EndingDate =new DateTime(studentInClass.StartDate.Year, 5, 31);

            //Учение может одновременно учиться только в одном классе, следовательно должен быть только один true, а остальные false
            foreach (StudentInClass s1 in db.StudentsInClasses.Where(e => e.Relevance == true))
                s1.Relevance = false;

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studentInClass.Id }, studentInClass);
        }

        // DELETE: api/StudentInClasses/5
        //Удаление ученика из класса
        [ResponseType(typeof(StudentInClass))]
        public IHttpActionResult DeleteStudentInClass(int id)
        {
            StudentInClass studentInClass = db.StudentsInClasses.Find(id);
            if (studentInClass == null)
            {
                return NotFound();
            }

            //db.StudentsInClasses.Remove(studentInClass);
            studentInClass.Relevance = false;

            /*Скорее всего надо сделать проверку, чтобы хотя бы один из классов был true,
             то есть ученик должен быть в одном классе. Но с другой стороны, он может окончить школу, 
             все 11 классов, может просто бросить школу, уйти в армию,
             а потом вновь вернутся - много возможных вариантов*/

            studentInClass.EndingDate = DateTime.Now; //Записываем реальную дату окончания обучения ученика в классе взамен теоретической
            db.SaveChanges();

            return Ok(studentInClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentInClassExists(int id)
        {
            return db.StudentsInClasses.Count(e => e.Id == id) > 0;
        }
    }
}