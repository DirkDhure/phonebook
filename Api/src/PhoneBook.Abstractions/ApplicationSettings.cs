
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Abstractions
{
  
    public interface IApplicationSettings
    {
        /// <summary>
        /// 
        /// </summary>
        string VirtualDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string CommandServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string CommandDatabaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string QueryServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string QueryDatabaseName { get; set; }
      
        /// <summary>
        /// 
        /// </summary>
        string DataStorage { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ApplicationSettings : IApplicationSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string VirtualDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommandServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommandDatabaseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QueryServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QueryDatabaseName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string DataStorage { get; set; }
    }


}
