using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace CalcAgeFunctionApp
{
	public static class CalcAgeFunction
	{
		[FunctionName("CalcAgeFunction")]
		public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
			HttpRequest req)
		{
			try
			{
				var jsonBirthDate = await req.ReadAsStringAsync().ConfigureAwait(false);
				var birthDate = GetUserDateTimeFromJson(jsonBirthDate);
				var result = CalcAge(birthDate);
				return new OkObjectResult(result);
			}
			catch (Exception ex)
			{
				return new BadRequestObjectResult(ex.Message);
			}
		}

		/// <summary>
		///		Calculate persons age
		/// </summary>
		/// <param name="birthDate"></param>
		/// <returns>Formatted string</returns>
		private static string CalcAge(DateTime birthDate)
		{
			var year = DateTime.Now.Year - birthDate.Year;
			var month = DateTime.Now.Month - birthDate.Month;
			if (month >= 0)
			{
				return $"Person is {year} year(s) and {month} month(s) old";
			}
			year--;
			month += 12;
			return $"Person is {year} year(s) and {month} month(s) old";
		}

		/// <summary>
		///     Convert JSON Dates written in the ISO 8601 format,
		///     e.g. "2012-03-21T05:40Z" to user datetime
		/// </summary>
		/// <param name="jsonDateTime"></param>
		/// <returns>DateTime</returns>
		private static DateTime GetUserDateTimeFromJson(string jsonDateTime)
		{
			if (string.IsNullOrEmpty(jsonDateTime))
			{
				return DateTime.MinValue;
			}
			var dateFormatSettings = new JsonSerializerSettings
			{
				DateFormatHandling = DateFormatHandling.IsoDateFormat
			};
			return JsonConvert.DeserializeObject<DateTime>(jsonDateTime, dateFormatSettings);
		}
	}
}