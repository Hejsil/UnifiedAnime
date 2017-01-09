﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace UnifiedAnime.Clients.Bases
{
    public class Parameters : IEnumerable<Parameter>
    {
        private readonly List<Parameter> _data = new List<Parameter>();

        public void Add(string name, object value) => _data.Add(new Parameter { Name = name, Value = value });
        public IEnumerator<Parameter> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
