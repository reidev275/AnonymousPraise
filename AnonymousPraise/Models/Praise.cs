using System;

namespace AnonymousPraise.Models
{
	public class Praise
	{
		public int Id { get; set; }
		public string Person { get; set; }
		public string Comment { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool Moderated { get; set; }
		public string Date { get { return CreatedDate.ToShortDateString(); }  }
		public int Likes { get; set; }
	}
}