using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Base.Entities.Infrastructure.AzureAD
{
    public class GraphUserResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public User? CreatedUser { get; set; }
        public string? ErrorCode { get; set; }

        public static GraphUserResult SuccessResult(User user) =>
            new() { Success = true, CreatedUser = user };

        public static GraphUserResult AlreadyExists(string message) =>
            new() { Success = false, Message = message, ErrorCode = "UserAlreadyExists" };

        public static GraphUserResult Failure(string errorCode, string message) =>
            new() { Success = false, ErrorCode = errorCode, Message = message };
    }

}
