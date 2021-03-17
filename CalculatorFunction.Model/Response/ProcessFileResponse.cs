namespace CalculatorFunction.Model.Response
{
    public class ProcessFileResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public static ProcessFileResponse SuccessResponse(string message)
        {
            return new ProcessFileResponse
            {
                Status = true,
                Message = message
            };
        }

        public static ProcessFileResponse ErrorResponse(string message)
        {
            return new ProcessFileResponse
            {
                Status = false,
                Message = message
            };
        }
    }
}