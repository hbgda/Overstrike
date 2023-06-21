﻿using DAT1.Sections;
using DAT1.Sections.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT1.Files
{
    public class Config : DAT1
    {
        public const uint MAGIC = 0x21A56F68;

        uint magic, dat1_size;
        byte[] unk;

        public Config(BinaryReader r): base()
        {
            magic = r.ReadUInt32();
            dat1_size = r.ReadUInt32();
            unk = r.ReadBytes(28);

            Debug.Assert(magic == MAGIC, "Config(): bad magic");

            Init(r);
        }

        public ConfigTypeSection TypeSection => (ConfigTypeSection)sections[ConfigTypeSection.TAG];
        public ConfigBuiltSection ContentSection => (ConfigBuiltSection)sections[ConfigBuiltSection.TAG];
        public ConfigReferencesSection ReferencesSection => (ConfigReferencesSection)sections[ConfigReferencesSection.TAG];

		public override byte[] Save() {
			var s = new MemoryStream();
			var w = new BinaryWriter(s);

			byte[] data = base.Save();
			w.Write((UInt32)magic);
			w.Write((UInt32)data.Length);
			w.Write(unk);
			w.Write(data);

			return s.ToArray();
		}
			/*

			@classmethod
			def make(cls):
			r = cls(io.BytesIO(cls.EMPTY_DATA))
			r.dat1.header.unk1 = cls.MAGIC
			r.dat1.add_string("Config Built File")
			return r
			*/
		}
}
