# Icod.GenKey
GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.

## Usage
`GenKey.exe --help`
Displays this text.

`GenKey.exe --copyright`
Displays copyright and licensing information.

`GenKey.exe [--len length] [--clip]`
GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.
The optional /len argument specifies the length of the key, in bytes.
The default key length is 32 bytes.
If present, the /clip argument specifies the output should also be copied to the clipboard.
Examples:
```sh
GenKey.exe
GenKey.exe --len 64
GenKey.exe --len 128 --clip
GenKey.exe --clip
```

## Copyright and Licensing
GenKey.exe generates a cryptographically secure sequence of hex-encoded bytes.
Copyright( C ) 2023 Timothy J. Bruce

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published 
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.