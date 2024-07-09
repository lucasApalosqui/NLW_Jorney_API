using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public abstract class JorneyException : SystemException
    {
        public JorneyException(string message) : base(message) 
        {

        }

        public abstract HttpStatusCode GetStatusCode();
        public abstract IList<string> GetErrorMessages();
    }
}
