using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HRPayroll.Models
{
    public class Common
    {
        [Serializable]
        public class Pager
        {
            public Pager(int totalItems, int? page, int pageSize = 10)
            {
                // calculate total, start and end pages
                var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
                var currentPage = page != null ? (int)page : 1;
                var startPage = currentPage - 5;
                var endPage = currentPage + 4;
                if (startPage <= 0)
                {
                    endPage -= (startPage - 1);
                    startPage = 1;
                }
                if (endPage > totalPages)
                {
                    endPage = totalPages;
                    if (endPage > 10)
                    {
                        startPage = endPage - 9;
                    }
                }

                TotalItems = totalItems;
                CurrentPage = currentPage;
                PageSize = pageSize;
                TotalPages = totalPages;
                StartPage = startPage;
                EndPage = endPage;
            }
            public int TotalItems { get; private set; }
            public int CurrentPage { get; private set; }
            public int PageSize { get; private set; }
            public int TotalPages { get; private set; }
            public int StartPage { get; private set; }
            public int EndPage { get; private set; }


        }

        public string ValidateImagePDF_FileExtWithSize(HttpPostedFileBase Uploadedfile, int ImageSize)
        {
            string massage;
            String fn = Path.GetFileNameWithoutExtension(Uploadedfile.FileName);
            String ext = Path.GetExtension(Uploadedfile.FileName);
            char[] SpecialChars = "!@#$%^&*()+=~`\\|/?><,\"".ToCharArray();
            int indexOf = fn.IndexOfAny(SpecialChars);
            String fileName = fn;
            int count = fileName.Split('.').Length - 1;
            if (count > 1)
            {
                massage = "Double extension not allowed in File name";
            }
            else
            {
                if (indexOf != -1)
                {
                    massage = "Special character not allowed in File name/फ़ाइल के नाम में विशेष वर्ण नहीं होने चाहिए।’";

                }
                else
                {
                    string mimetype = Uploadedfile.ContentType;
                    if ((ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg") && (mimetype == "image/jpeg" || mimetype == "image/jpg"))
                    {
                        fn = "";

                        string fFullName = Uploadedfile.FileName;
                        int len = fFullName.Length;
                        string ext1 = Path.GetExtension(fFullName);
                        string str = fFullName.Substring(fFullName.LastIndexOf("\\") + 1);
                        len = str.Length;
                        string fileN = str.Substring(0, len - ext1.Length);

                        Regex FilenameRegex = null;
                        FilenameRegex = new Regex("(.*?)\\.(jpeg|jpg|JPEG|JPG)$", RegexOptions.IgnoreCase);
                        int index = fileN.IndexOf(".");

                        if (!FilenameRegex.IsMatch(fFullName) || index != -1)
                        {
                            massage = "Please upload .jpg, .jpeg or .pdf File only";
                        }
                        else
                        {
                            string Photoname = Path.GetFileNameWithoutExtension(Uploadedfile.FileName);
                            string fileSize = Uploadedfile.ContentLength.ToString();
                            String ImageFileNameMBA = Uploadedfile.FileName;

                            Byte[] stu_imageMBA = new Byte[Uploadedfile.ContentLength];

                            Stream fs = Uploadedfile.InputStream;
                            fs.Read(stu_imageMBA, 0, Convert.ToInt32(fileSize));

                            fs.Seek(0, SeekOrigin.Begin);
                            StreamReader sr = new StreamReader(fs, true);

                            string firstLine = sr.ReadLine().ToString();

                            //string firstLine = "JFIF";

                            if ((firstLine.IndexOf("JFIF") > -1) || (firstLine.IndexOf("Exif") > -1))
                            {

                                if (Uploadedfile.ContentLength <= 1024 * ImageSize)
                                {
                                    massage = "Valid";
                                }
                                else
                                {
                                    massage = "File size can not exceed " + ImageSize + " KB ";
                                }
                            }
                            else
                            {
                                massage = "Please upload .jpg, .jpeg or .pdf File only";
                            }
                        }
                    }
                    else if (ext.ToLower() == ".pdf" && mimetype == "application/pdf")
                    {
                        fn = "";

                        string fFullName = Uploadedfile.FileName;
                        int len = fFullName.Length;
                        string ext1 = Path.GetExtension(fFullName);
                        string str = fFullName.Substring(fFullName.LastIndexOf("\\") + 1);
                        len = str.Length;
                        string fileN = str.Substring(0, len - ext1.Length);

                        Regex FilenameRegex = null;
                        //FilenameRegex = new Regex("(.*?)\\.(jpeg|jpg|JPEG|JPG)$", RegexOptions.IgnoreCase);
                        FilenameRegex = new Regex("(.*?)\\.(pdf|PDF)$", RegexOptions.IgnoreCase);
                        int index = fileN.IndexOf(".");

                        if (!FilenameRegex.IsMatch(fFullName) || index != -1)
                        {
                            massage = "Please upload .jpg, .jpeg or .pdf File only";
                        }
                        else
                        {
                            string Photoname = Path.GetFileNameWithoutExtension(Uploadedfile.FileName);
                            string fileSize = Uploadedfile.ContentLength.ToString();
                            String ImageFileNameMBA = Uploadedfile.FileName;

                            Byte[] stu_imageMBA = new Byte[Uploadedfile.ContentLength];

                            Stream fs = Uploadedfile.InputStream;
                            fs.Read(stu_imageMBA, 0, Convert.ToInt32(fileSize));

                            fs.Seek(0, SeekOrigin.Begin);
                            StreamReader sr = new StreamReader(fs, true);

                            string firstLine = sr.ReadLine().ToString();

                            //string firstLine = "JFIF";

                            if ((firstLine.IndexOf("%PDF") > -1))
                            {

                                if (Uploadedfile.ContentLength <= 1024 * ImageSize)
                                {
                                    massage = "Valid";
                                }
                                else
                                {
                                    massage = "File size can not exceed " + ImageSize + " KB ";
                                }
                            }
                            else
                            {
                                massage = "Please upload .jpg, .jpeg or .pdf File only";
                            }
                        }
                    }


                    else
                    {
                        massage = "Please upload .jpg, .jpeg or .pdf File only";
                    }
                }
            }
            return massage;
        }
    }
}