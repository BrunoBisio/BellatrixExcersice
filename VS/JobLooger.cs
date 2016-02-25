using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace bellatrixLog
{
    public class JobLooger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDataBase;
        
        public JobLooger(bool logToFile, bool logToConsole, bool logToDataBase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDataBase = logToDataBase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        public void LogMessage(string message, bool isMessage, bool isWarning, bool isError)
        {
            string checkMessage = "";

            if (message != null)
            {
                checkMessage = message.Trim();
            }

            if (checkMessage == "" || checkMessage.Length == 0)
            {
                return;
            }

            if (!_logToConsole && !_logToFile && !_logToDataBase)
            {
                throw new Exception("Invalid Configuration");
            }

            if ((!_logError && !_logMessage && !_logWarning) || (!isMessage && !isWarning && !isError))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                connection.Open();
                int logType = 0;

                if (isMessage && _logMessage)
                {
                    logType = 1;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (isError && _logError)
                {
                    logType = 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (isWarning && _logWarning)
                {
                    logType = 3;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                SqlCommand command = new SqlCommand("Insert into log Values('" + message + "'," + logType.ToString() + ")");
                command.ExecuteNonQuery();

                string loggedText = "";

                if (!File.Exists(ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
                {
                    loggedText = File.ReadAllText(ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
                }

                loggedText = loggedText + DateTime.Now.ToShortDateString() + message;
                File.WriteAllText(ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", loggedText);
                Console.WriteLine(DateTime.Now.ToShortDateString() + message);
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new ConfigurationErrorsException(ex.StackTrace);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.StackTrace);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.StackTrace);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException(ex.StackTrace);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(ex.StackTrace);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.StackTrace);
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException(ex.StackTrace);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.StackTrace);
            }
        }
    }
}