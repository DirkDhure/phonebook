namespace PhoneBook.Abstractions
{
    public class LoggingContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="apiName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="statusCode"></param>
        public LoggingContext(string serverName, string apiName, string controllerName, string actionName, int statusCode)
        {
            this.ServerName = serverName;
            this.APIName = apiName;
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string APIName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; }
    }
}
