using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace ClubeDoLivro.Function.Abstractions
{
	public static class HttpRequestExtensions
	{
		public static TValue GetObjectFromQueryString<TValue>(this HttpRequestData httpRequestData) where TValue : new()
		{
			var result = new TValue();
			var properties = typeof(TValue).GetProperties();
			var requestQuery = HttpUtility.ParseQueryString(httpRequestData.Url.Query);

			foreach (string parameterName in requestQuery.Keys)
			{
				var property = properties.FirstOrDefault(p => p.Name == parameterName);
				if (property != null && property.CanWrite)
				{
					var value = requestQuery.GetValue(parameterName, property.PropertyType);
					property.SetValue(result, value);
				}
			}
			return result;
		}

		public static TValue GetValueFromQueryString<TValue>(this HttpRequestData httpRequestData, string parameterName)
		{
			var requestQuery = HttpUtility.ParseQueryString(httpRequestData.Url.Query);

			return (TValue)requestQuery.GetValue(parameterName, typeof(TValue));
		}

		public static TValue GetObjectFromHeader<TValue>(this HttpRequestData httpRequestData) where TValue : new()
		{
			var result = new TValue();
			var properties = typeof(TValue).GetProperties();

			foreach (var header in httpRequestData.Headers)
			{
				var property = properties.FirstOrDefault(p => p.Name == header.Key);
				if (property != null && property.CanWrite)
				{
					var value = property.PropertyType.GetValue(header.Value.FirstOrDefault());
					property.SetValue(result, value);
				}
			}
			return result;
		}

		public static TValue GetValueFromHeader<TValue>(this HttpRequestData httpRequestData, string headerName)
		{
			var headerValue = httpRequestData.Headers.FirstOrDefault(x => x.Key == headerName);
			var value = headerValue.Value?.FirstOrDefault() ?? "";
			return (TValue)typeof(TValue).GetValue(value);
		}

		public static async Task<TValue> GetObjectFromBody<TValue>(this HttpRequestData httpRequestData)
		{
			using var streamReader = new StreamReader(httpRequestData.Body);
			var jsonString = await streamReader.ReadToEndAsync();
			return JsonConvert.DeserializeObject<TValue>(jsonString);
		}

		public static async Task<HttpResponseData> OkResponse(this HttpRequestData httpRequestData, object value)
		{
			return await httpRequestData.GenericResponse(HttpStatusCode.OK, value);
		}

		public static async Task<HttpResponseData> CreatedResponse(this HttpRequestData httpRequestData, string location, object value)
		{
			return await httpRequestData.GenericResponse(HttpStatusCode.Created, value);
		}

		public static async Task<HttpResponseData> BadRequestResponse(this HttpRequestData httpRequestData, object value)
		{
			return await httpRequestData.GenericResponse(HttpStatusCode.BadRequest, value);
		}

		public static async Task<HttpResponseData> GenericResponse(this HttpRequestData httpRequestData, HttpStatusCode httpStatusCode, object value)
		{
			var response = httpRequestData.CreateResponse();
			if (value is not null)
				await response.WriteAsJsonAsync(value);
			response.StatusCode = httpStatusCode;
			return response;
		}

		private static object GetValue(this NameValueCollection requestQuery, string parameterName, Type type)
		{
			var parameterValue = requestQuery[parameterName];

			return type.GetValue(parameterValue);
		}

		private static object GetValue(this Type type, string parameterValue)
		{
			return type.IsEnum
				? Enum.Parse(type, parameterValue)
				: Convert.ChangeType(parameterValue, type, FormatProviders.enUS);
		}
	}

}