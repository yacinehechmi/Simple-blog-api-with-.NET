namespace Tidjma.Helpers;

/*

   1/ Create a wrapper around returned results call it OperationResult<T>
   3/ Map exveptions to HTTP results 400, 404, 500, 403, etc

*/

enum Errors
{
    NotFound = 0,
    Validation = 1,
    Conflict = 2,
    Failure = 3
}

/*

   failed operation with exception
   failed operation without exception (with a message)
   successfull operation 

*/

public class OperationResult<T> 
{
    public bool IsSuccess  { get; set; }
    public Exception? ExceptionType  { get; set; }
    public string? ErrorMessage  { get; set; }
    public T? Payload { get; set; } // sometimes this will be null and the operation will be successfull
    public List<Exception>? GetExceptions(Exception e = null, List<Exception> exceptionCollection = null, bool firstIter = true)
    {
        var innerException = firstIter ? this.ExceptionType.InnerException : e.InnerException;
        if (innerException is null) { return exceptionCollection; } 

        firstIter = false;
        exceptionCollection.Add(innerException); 

        return GetExceptions(innerException, exceptionCollection, firstIter);
    }
}
