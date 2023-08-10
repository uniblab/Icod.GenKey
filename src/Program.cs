// GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.
// Copyright( C ) 2023 Timothy J. Bruce

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

		private const System.Int32 theDefaultKeyLen = 32;

		[System.STAThread]
		public static System.Int32 Main( System.String[] args ) {
			if ( null == args ) {
				args = new System.String[ 0 ];
			}

			Pair<System.Int32, System.Boolean>? cmd;
			if ( args.Any() ) {
				try {
					cmd = ParseInput( args );
					if ( null == cmd ) {
						return 1;
					}
				} catch ( System.Exception e ) {
					System.Console.Error.WriteLine( e.Message );
					return 1;
				}
			} else {
				cmd = new Pair<System.Int32, System.Boolean>( theDefaultKeyLen, false );
			}

			var len = cmd.First;
			if ( len < 1 ) {
				PrintUsage();
				return 1;
			}
			var data = new System.Byte[ len ];
			using ( var provider = System.Security.Cryptography.RandomNumberGenerator.Create() ) {
				provider.GetBytes( data );
			}
			var builder = new System.Text.StringBuilder( len << 2 );
			foreach ( var b in data ) {
				builder = builder.AppendFormat( "{0:x2}", b );
			}
			var text = builder.ToString();
			if ( cmd.Second ) {
				new TextCopy.Clipboard().SetText( text );
			}
			System.Console.Out.WriteLine( text );
			return 0;
		}

		private static Pair<System.Int32, System.Boolean>? ParseInput( System.String[] args ) {
			var clip = false;
			var len = theDefaultKeyLen;
			System.String @switch;
			var i = -1;
			var length = args.Length - 1;
			do {
				@switch = args[ ++i ];
				if ( new System.String[] { "--help", "-h", "/h" }.Contains( @switch, System.StringComparer.OrdinalIgnoreCase ) ) {
					PrintUsage();
					return null;
				} else if ( new System.String[] { "--copyright", "-c", "/c" }.Contains( @switch, System.StringComparer.OrdinalIgnoreCase ) ) {
					PrintCopyright();
					return null;
				} else if (
					( "--len".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) )
					|| ( "/len".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) )
				) {
					len = System.Convert.ToInt32( args[ ++i ] );
					if ( len < 1 ) {
						PrintUsage();
						return null;
					}
				} else if (
					( "--clip".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) )
					|| ( "/clip".Equals( @switch, System.StringComparison.OrdinalIgnoreCase ) )
				) {
					clip = true;
				} else {
					PrintUsage();
					return null;
				}
			} while ( i < length );
			return new Pair<System.Int32, System.Boolean>( len, clip );
		}

		private static void PrintUsage() {
			System.Console.Error.WriteLine( "No, no, no! Use it like this, Einstein:" );
			System.Console.Error.WriteLine( "GenKey.exe [/len length] [/clip]" );
			System.Console.Error.WriteLine( "GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes." );
			System.Console.Error.WriteLine( "The optional /len argument specifies the length of the key, in bytes." );
			System.Console.Error.WriteLine( "The default key length is 32 bytes." );
			System.Console.Error.WriteLine( "If present, the /clip argument specifies the output should also be copied to the clipboard." );
			System.Console.Error.WriteLine( "Example: GenKey.exe" );
			System.Console.Error.WriteLine( "Example: GenKey.exe /len 64" );
			System.Console.Error.WriteLine( "Example: GenKey.exe /len 128 /clip" );
			System.Console.Error.WriteLine( "Example: GenKey.exe /clip" );
		}
		private static void PrintCopyright() {
			var copy = new System.String[] {
				"GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.",
				"",
				"Copyright( C ) 2023 Timothy J. Bruce",
				"",
				"This program is free software: you can redistribute it and / or modify",
				"it under the terms of the GNU General Public License as published by",
				"the Free Software Foundation, either version 3 of the License, or",
				"( at your option ) any later version.",
				"",
				"",
				"This program is distributed in the hope that it will be useful,",
				"but WITHOUT ANY WARRANTY; without even the implied warranty of",
				"",
				"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the",
				"",
				"GNU General Public License for more details.",
				"",
				"",
				"You should have received a copy of the GNU General Public License",
				"",
				"along with this program.If not, see < https://www.gnu.org/licenses/>."
			};
			foreach ( var line in copy ) {
				System.Console.WriteLine( line );
			}
		}

	}

}