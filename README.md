# IniUtils
ini file utility

## Feature

- INIファイル同士の比較が可能
- コメントを扱える
- 読み書きが速い（GetPrivateProfileString、WritePrivateProfileStringと比較して）


## Usage

``` C#
IniFile ini1 = IniFileParser.ParseIniFile(iniFilePath1);
IniFile ini2 = IniFileParser.ParseIniFile(iniFilePath2);
// Diff ini files
IniFile sub = ini1 - ini2;

// write ini file
sub.Export(outputPath);
```
