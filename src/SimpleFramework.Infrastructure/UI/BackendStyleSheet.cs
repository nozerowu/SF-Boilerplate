﻿
namespace SimpleFramework.Infrastructure.UI
{
  public class BackendStyleSheet
  {
    public string Url { get; set; }
    public int Position { get; set; }

    public BackendStyleSheet(string url, int position)
    {
      this.Url = url;
      this.Position = position;
    }
  }
}