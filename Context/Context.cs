using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using DataModel.BBDD;
using DataModel.ViewModels;

namespace DataModel.Models
{
    public class Entities
    {
        public static NutrisandEntities getContext()
        {
            return new NutrisandEntities();
        }

        public static Return saveAndReturn(NutrisandEntities db)
        {
            try
            {
                db.SaveChanges();
                return new Return(true, "Perfect!");
            }
            catch (DbEntityValidationException ex)
            {
                var val = Validator.ToString(ex);
                Log.SimpleLog("Error: " + val);
                return new Return(false, Validator.ToString(ex));
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? (ex.InnerException.InnerException != null) ? "Error: " + ex.InnerException.InnerException.Message : "Error: " + ex.InnerException.Message : ex.Message;
                Log.SimpleLog(innerMessage);
                return new Return(false, innerMessage);
            }
        }
    }
}

