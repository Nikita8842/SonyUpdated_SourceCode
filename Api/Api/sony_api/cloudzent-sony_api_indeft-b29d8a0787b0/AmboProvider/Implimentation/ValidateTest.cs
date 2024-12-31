using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelper;
using AmboProvider.Interface;

namespace AmboProvider.Implimentation
{
    public class ValidateTest : IValidateTest
    {
        private readonly DbHelper _dbHelper;
        public ValidateTest()
        {
            _dbHelper = DbHelper.GetInstance("defaultConnection");
        }
                
        public int IValidateTest()
        {
            return 1;
        }
    }
}
