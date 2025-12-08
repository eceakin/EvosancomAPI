using EvosancomAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Infrastructure.Services.Storage.Local
{
	public class LocalStorage : ILocalStorage
	{
		readonly IWebHostEnvironment _webHostEnvironment;

		public LocalStorage(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task DeleteAsync(string pathOrContainerName, string fileName)
		{
			File.Delete($"{pathOrContainerName} \\ {fileName}");
		}

		public List<string> GetFiles(string pathOrContainerName)
		{
			DirectoryInfo directoryInfo = new(pathOrContainerName);
			return directoryInfo.GetFiles().Select(f => f.Name).ToList();
		}

		public bool HasFile(string pathOrContainerName, string fileName)
		=>File.Exists($"{pathOrContainerName} \\ {fileName}");

		async Task<bool> CopyFilesAsync(string path, IFormFile file)
		{
			try
			{
				await using FileStream fileStream =
					new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
				await file.CopyToAsync(fileStream);
				await fileStream.FlushAsync();
				return true;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
		public async Task<List<(string fileName, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
		{
			string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathOrContainerName);
			if (!Directory.Exists(uploadPath))
			{
				Directory.CreateDirectory(uploadPath);
			}
			List<(string fileName, string path)> datas = new();
			List<bool> resultss = new();
			foreach (IFormFile file in files)
			{
				bool result = await CopyFilesAsync($"{uploadPath} \\ {file.Name}", file);
				datas.Add((file.Name, $"{pathOrContainerName} \\ {file.Name}"));
			}
			return datas;

		}
	}
}
