using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace learning.Models
{
    public class Master
    {

    }
    public class Student
    {
        public int SId { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Addr { get; set; }
        public string Mob { get; set; }
        public string Photo { get; set; }
        public string PhotoPath { get; set; }
        public string SignaturePath { get; set; }
        public string flag { get; set; }
        public string msg { get; set; }
        public List<Student> list { get; set; }
  
        
    }

    public class state
    {
        public int Value { get; set; }
        public String Text { get; set; }
        public List <state> lst { get; set; }
    }

    public class MultipleFileUploading
    {
        public string File { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public List<MultipleFileUploading> List { get; set; }
    }
}