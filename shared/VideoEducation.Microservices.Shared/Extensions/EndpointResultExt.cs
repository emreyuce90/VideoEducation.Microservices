using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VideoEducation.Microservices.Shared.Extensions
{
    public static class EndpointResultExt
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> serviceResult)
        {
            return serviceResult.Status switch
            {

                HttpStatusCode.OK => Results.Ok(serviceResult.Data),
                HttpStatusCode.Created => Results.Created(serviceResult.Url, serviceResult.Data),
                HttpStatusCode.NotFound => Results.NotFound(serviceResult.Fail!),
                //default case dir eğer yukarıdaki kısımlara uymaz ise aşağıdaki kod bloğu çalıştırılır
                _ => Results.Problem(serviceResult.Fail!)
                //null - forgiving operator derleyiciye buraya muhakak Fail in dolu geleceğini garanti ediyorum
            };
        }

        public static IResult ToGenericResult(this ServiceResult serviceResult)
        {
            return serviceResult.Status switch
            {

                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(serviceResult.Fail!),
                _ => Results.Problem(serviceResult.Fail!)
            };
        }
    }
}
