using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.IO;

namespace Panja_Project
{
    class TestClass
    {

        LinkedList<string> file_name;

       public TestClass()
        {
           file_name = new LinkedList<string>();
        }


        public void addList(string file_dir)
        {

            if (file_name.Contains(file_dir))
            {
                //all ready saved
            }
            else
            {
                //new list

                file_name.AddLast(file_dir);
            }
        }


        public void minusList(string file_dir)
        {

            if (file_name.Contains(file_dir)){
                file_name.Remove(file_dir);
            }
            else
            {
                //empty
            }
        }


        public string export()
        {
            return file_name.ToString();
        }

    }

}
