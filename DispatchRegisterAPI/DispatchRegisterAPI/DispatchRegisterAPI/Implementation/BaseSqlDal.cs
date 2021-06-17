using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DispatchRegisterAPI.Implementation

{
    public abstract class BaseSQLDAL
    {
        protected SQLDAL obj_sqldal;
        public BaseSQLDAL()
        {
            InstanTiateDAL();
        }
        private void InstanTiateDAL()
        {
            if (obj_sqldal == null)
            {
                obj_sqldal = new SQLDAL();
            }
        }
    } 

}
