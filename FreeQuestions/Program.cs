using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FreeQuestions
{
    internal class Program
    {

        public static string libsPath = @"..\..\..\libs";

        public static List<Questions> questions = new List<Questions>();
        public static List<exam> exams = new List<exam>();
        public static List<subject> subjects = new List<subject>();
        public static string jsonBasePath = Path.Combine(libsPath, "output");
        public static string questionsPath = Path.Combine(jsonBasePath, "questions.json");
        public static string subjectPath = Path.Combine(jsonBasePath, "subject.json");
        public static string examPath = Path.Combine(jsonBasePath, "exam.json");
        public static string fileBasePath = Path.Combine(libsPath, "input");

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var examLines = File.ReadAllLines(examPath);
            foreach (var line in examLines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    exams.Add(JsonConvert.DeserializeObject<exam>(line));
                }
            }

            var subjectLines = File.ReadAllLines(subjectPath);
            foreach (var line in subjectLines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    subjects.Add(JsonConvert.DeserializeObject<subject>(line));
                }
            }


            var questionsLines = File.ReadAllLines(questionsPath);
            foreach (var line in questionsLines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    questions.Add(JsonConvert.DeserializeObject<Questions>(line));
                }
            }

            JsonToFile(exams, subjects, questions);
            FileToJson();
        }

        public static void FileToJson()
        {
            var regex = new Regex("^[0-9a-zA-Z]+");
            var numsregex = new Regex("^[0-9]+$");
            List<Questions> questions = new List<Questions>();
            List<exam> exams = new List<exam>();
            List<subject> subjects = new List<subject>();
            DirectoryInfo di = new DirectoryInfo(fileBasePath);
            foreach (var dis in di.GetDirectories())
            {

                var matchExamResult = regex.Matches(dis.Name);
                if (matchExamResult.Any())
                {
                    var examId = matchExamResult[0].Value;
                    var examName = dis.Name.Replace(examId, "");
                    exams.Add(new exam { id = examId, name = examName });
                    foreach (var subDis in dis.GetDirectories())
                    {
                        var matchSubjectResult = regex.Matches(subDis.Name);
                        if (matchSubjectResult.Any())
                        {
             
                            var subjectid = matchSubjectResult[0].Value;
                            var subjectName = subDis.Name.Replace(subjectid, "");
                          
                            var files = subDis.GetFiles();
                            var fileCount = 0;
                            foreach (var fileItem in files)
                            {
                                var order = Path.GetFileNameWithoutExtension(fileItem.FullName);//题目顺序
                                if (numsregex.IsMatch(order))
                                {
                                    fileCount++;
                                    var strQues = File.ReadAllText(fileItem.FullName);
                                    var jsonQues = JsonConvert.DeserializeObject<Questions>(strQues);
                                    jsonQues.order = order;
                                    jsonQues.examid = examId;
                                    jsonQues.subjectid = subjectid;

                                    questions.Add(jsonQues);
                                }

                            }
                            if (fileCount>0)
                            {
                                subjects.Add(new subject { id = subjectid, name = subjectName, examid = examId, counts = fileCount });
                            }
                
                        }

                    }
                }
                else
                {
                    Console.WriteLine($"exam文件夹的命名格式不正常:{dis.Name}");
                }
            }
            File.WriteAllLines(questionsPath,questions.Select(c=>JsonConvert.SerializeObject(c)));
            File.WriteAllLines(subjectPath, subjects.Select(c => JsonConvert.SerializeObject(c)));
            File.WriteAllLines(examPath, exams.Select(c => JsonConvert.SerializeObject(c)));
        }



        public static Dictionary<string, string> examMap = new Dictionary<string, string>();

        /// <summary>
        /// 将json文件转换成文件结构
        /// </summary>
        /// <param name="exams"></param>
        /// <param name="subjects"></param>
        /// <param name="questions"></param>
        public static void JsonToFile(List<exam> exams, List<subject> subjects, List<Questions> questions)
        {
            foreach (var item in exams)
            {
                var dirRoot = Path.Combine(fileBasePath, item.id + item.name);
                if (!Directory.Exists(dirRoot))
                {
                    Directory.CreateDirectory(dirRoot);
                }
                if (!examMap.ContainsKey(item.id))
                {
                    examMap.Add(item.id, item.id + item.name);

                }

            }
            foreach (var item in subjects)
            {
                if (examMap.ContainsKey(item.examid))
                {
                    var subRoot = Path.Combine(fileBasePath, examMap[item.examid], item.id+item.name);
                    if (!Directory.Exists(subRoot))
                    {
                        Debug.WriteLine($"创建的路径是 {subRoot}");
                        Directory.CreateDirectory(subRoot);
                    }
                    var fileitems = questions.Where(c => c.examid == item.examid && c.subjectid == item.id).ToList();
                    foreach (var question in fileitems)
                    {
                        File.WriteAllText(Path.Combine(subRoot, question.order + ".json"), JsonConvert.SerializeObject(question));
                    }
                }
                else
                {
                    throw new Exception($"没有找到试卷{item.examid}分类");
                }


            }

        }
    }
}