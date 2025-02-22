// GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.
// Copyright( C ) 2025 Timothy J. Bruce

/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

namespace Icod.GenKey {

	public static class GenKey {

		#region nested classes
		private sealed class Pair<S, T> {
			private readonly S myFirst;
			private readonly T mySecond;
			public Pair( S first, T second ) : base() {
				myFirst = first;
				mySecond = second;
			}
			public S First {
				get {
					return myFirst;
				}
			}
			public T Second {
				get {
					return mySecond;
				}
			}
		}
		#endregion nested classes

		private const System.String theDefaultKeyLen = "32";

		[System.STAThread]
		public static System.Int32 Main( System.String[] args ) {
			var len = args.Length;
			if ( 3 < len ) {
				PrintUsage();
				return 1;
			}

			var processor = new Icod.Argh.Processor(
				new Icod.Argh.Definition[] {
					new Icod.Argh.Definition( "help", new System.String[] { "-h", "--help", "/help" } ),
					new Icod.Argh.Definition( "copyright", new System.String[] { "-c", "--copyright", "/copyright" } ),
					new Icod.Argh.Definition( "len", new System.String[] { "-l", "--len", "/len" } ),
					new Icod.Argh.Definition( "clip", new System.String[] { "--clip", "/clip" } ),
				},
				System.StringComparer.OrdinalIgnoreCase
			);
			processor.Parse( args );

			if ( processor.Contains( "help" ) ) {
				PrintUsage();
				return 1;
			} else if ( processor.Contains( "copyright" ) ) {
				PrintCopyright();
				return 1;
			}

			if ( !processor.TryGetValue( "len", true, out var lenStr ) ) {
				lenStr = theDefaultKeyLen;
			}
			if (
				( System.String.IsNullOrEmpty( lenStr ) )
				|| ( !System.Int32.TryParse( lenStr, out var keyLen ) )
			) {
				PrintUsage();
				return 1;
			}
			var data = new System.Byte[ keyLen ];
			using ( var provider = System.Security.Cryptography.RandomNumberGenerator.Create() ) {
				provider.GetBytes( data );
			}
			var builder = new System.Text.StringBuilder( keyLen << 2 );
			foreach ( var b in data ) {
				builder = builder.AppendFormat( "{0:x2}", b );
			}
			var text = builder.ToString();
			System.Console.Out.WriteLine( text );
			if ( processor.Contains( "clip" ) ) {
				new TextCopy.Clipboard().SetText( text );
			}
			return 0;
		}

		private static void PrintUsage() {
			System.Console.Error.WriteLine( "No, no, no! Use it like this, Einstein:" );
			System.Console.Error.WriteLine( "GenKey.exe (-h | --help | /help)" );
			System.Console.Error.WriteLine( "GenKey.exe (-c | --copyright | /copyright)" );
			System.Console.Error.WriteLine( "GenKey.exe [(-l | --len | /len) length] [(--clip | /clip)]" );
			System.Console.Error.WriteLine( "GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes." );
			System.Console.Error.WriteLine( "The optional len argument specifies the length of the key, in bytes." );
			System.Console.Error.WriteLine( "The default key length is 32 bytes." );
			System.Console.Error.WriteLine( "If present, the clip argument specifies the output should also be copied to the clipboard." );
			System.Console.Error.WriteLine( "Example: GenKey.exe" );
			System.Console.Error.WriteLine( "Example: GenKey.exe --len 64" );
			System.Console.Error.WriteLine( "Example: GenKey.exe --len 128 --clip" );
			System.Console.Error.WriteLine( "Example: GenKey.exe --clip" );
		}
		private static void PrintCopyright() {
			var copy = new System.String[] {
				"GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.",
				"Copyright( C ) 2025 Timothy J. Bruce",
				"",
				"This program is free software: you can redistribute it and / or modify",
				"it under the terms of the GNU General Public License as published by",
				"the Free Software Foundation, either version 3 of the License, or",
				"( at your option ) any later version.",
				"",
				"This program is distributed in the hope that it will be useful,",
				"but WITHOUT ANY WARRANTY; without even the implied warranty of",
				"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the",
				"GNU General Public License for more details.",
				"",
				"You should have received a copy of the GNU General Public License",
				"along with this program.If not, see <https://www.gnu.org/licenses/>."
			};
			foreach ( var line in copy ) {
				System.Console.WriteLine( line );
			}
		}

	}

}