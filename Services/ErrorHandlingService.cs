namespace API_first_project.Services
{
    public class ErrorHandlingService<T> : IErrorHandlingService<T>
    {
        private T _error;

        public void SetError(T error)
        {
            _error = error;
        }

        public T GetError() => _error;
    }

}
