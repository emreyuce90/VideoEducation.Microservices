
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace VideoEducation.Microservices.Shared {
    public class ServiceResult {
        [JsonIgnore] public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }

        [JsonIgnore] public bool IsSuccess => Fail is null;
        [JsonIgnore] public bool IsFail => !IsSuccess;

        // Returns 201
        public static ServiceResult SuccessAsNoContent() {
            return new ServiceResult { Status = HttpStatusCode.NoContent };
        }

        // Returns 404
        public static ServiceResult ErrorAsNotFound() {
            return new ServiceResult() {
                Status = HttpStatusCode.NotFound,
                Fail = new ProblemDetails {
                    Title = "Not Found",
                    Detail = "The requested resource was not found"
                }
            };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode) {

            return new ServiceResult {

                Status = httpStatusCode,
                Fail = problemDetails
            };
        }

        // Returns title,description and given status code
        public static ServiceResult Error(string title, string description, HttpStatusCode httpStatusCode) {
            return new ServiceResult {
                Fail = new ProblemDetails() {
                    Title = title,
                    Detail = description,
                    Status = httpStatusCode.GetHashCode()
                },
                Status = httpStatusCode
            };
        }

        // Returns only title and status code
        public static ServiceResult Error(string title, HttpStatusCode httpStatusCode) {
            return new ServiceResult {
                Fail = new ProblemDetails() {
                    Title = title,
                    Status = httpStatusCode.GetHashCode()
                },
                Status = httpStatusCode
            };
        }

        //Fluent validtor errors reflected to error response
        public static ServiceResult ErrorFromValidator(IDictionary<string, object?> validationErrors) {
            return new ServiceResult {
                Fail = new ProblemDetails() {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = validationErrors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                },
                Status = HttpStatusCode.BadRequest
            };
        }
        //api exception error
        public static ServiceResult ErrorFromProblemDetails(Refit.ApiException apiException) {

            //eğer refitten api exception boş gelirse
            if (String.IsNullOrEmpty(apiException.Content)) {
                return new ServiceResult() {
                    Status = apiException.StatusCode,
                    Fail = new ProblemDetails { Title = apiException.Message }
                };
            }

            //dolu gelirse
            var exception = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            return new ServiceResult() {
                Status = apiException.StatusCode,
                Fail = exception
            };

        }

    }

    //return ServiceResult<Category>(category)
    public class ServiceResult<T> : ServiceResult {
        public T? Data { get; set; }
        public string? Url { get; set; }

        //200 + data
        public static ServiceResult<T> SuccessAsOk(T data) {
            return new ServiceResult<T>() {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }

        // 201 + data
        public static ServiceResult<T> SuccessAsCreated(T data, string url) {
            return new ServiceResult<T> {
                Status = HttpStatusCode.Created,
                Url = url,
                Data = data
            };
        }

        //400
        public static new ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode httpStatusCode) {

            return new ServiceResult<T> {

                Status = httpStatusCode,
                Fail = problemDetails
            };
        }


        public static  new ServiceResult<T> Error(string title, string description, HttpStatusCode httpStatusCode) {
            return new ServiceResult<T> {
                Fail = new ProblemDetails() {
                    Title = title,
                    Detail = description,
                    Status = httpStatusCode.GetHashCode()
                },
                Status = httpStatusCode
            };
        }

        public static new ServiceResult<T> Error(string title, HttpStatusCode httpStatusCode) {
            return new ServiceResult<T> {
                Fail = new ProblemDetails() {
                    Title = title,
                    Status = httpStatusCode.GetHashCode()
                },
                Status = httpStatusCode
            };
        }

        //Customized Errors
        public static new ServiceResult<T> ErrorFromValidator(IDictionary<string, object?> validationErrors) {
            return new ServiceResult<T> {
                Fail = new ProblemDetails() {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = validationErrors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                },
                Status = HttpStatusCode.BadRequest
            };
        }
        public static new ServiceResult<T> ErrorFromProblemDetails(Refit.ApiException apiException) {

            //eğer refitten api exception boş gelirse
            if (String.IsNullOrEmpty(apiException.Content)) {
                return new ServiceResult<T>() {
                    Status = apiException.StatusCode,
                    Fail = new ProblemDetails { Title = apiException.Message }
                };
            }

            //dolu gelirse
            var exception = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            return new ServiceResult<T>() {
                Status = apiException.StatusCode,
                Fail = exception
            };

        }

    }
}
