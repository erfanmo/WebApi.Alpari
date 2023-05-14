using System;
using System.Collections.Generic;

namespace WebApi.Alpari.Models.Dto
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime inserttime { get; set; }
        public List<Links> links { get; set; }
    }
}
