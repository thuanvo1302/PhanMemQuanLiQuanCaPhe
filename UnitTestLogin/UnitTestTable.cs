using BUS;
using DAL;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for UnitTestTable
    /// </summary>
    [TestClass]
    public class UnitTestTable
    {
       
        [TestMethod]
        public void TestInsertTable()
        {
            DAL_Table table = new DAL_Table();
            Assert.IsTrue(table.InsertTable());
        }
        
        [TestMethod]
        public void TestDeleteTable()
        {
            DAL_Table table = new DAL_Table();
            int table_id = 0;

            Assert.IsFalse(table.DeleteTable(table_id));
        }
    }
}
