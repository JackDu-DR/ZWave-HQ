; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [344 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [682 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 69
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 68
	i32 10266594, ; 2: LiveChartsCore.SkiaSharpView.dll => 0x9ca7e2 => 182
	i32 15721112, ; 3: System.Runtime.Intrinsics.dll => 0xefe298 => 109
	i32 32687329, ; 4: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 263
	i32 34715100, ; 5: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 297
	i32 34839235, ; 6: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 49
	i32 38948123, ; 7: ar\Microsoft.Maui.Controls.resources.dll => 0x2524d1b => 305
	i32 39109920, ; 8: Newtonsoft.Json.dll => 0x254c520 => 205
	i32 39485524, ; 9: System.Net.WebSockets.dll => 0x25a8054 => 81
	i32 42244203, ; 10: he\Microsoft.Maui.Controls.resources.dll => 0x284986b => 314
	i32 42639949, ; 11: System.Threading.Thread => 0x28aa24d => 146
	i32 66541672, ; 12: System.Diagnostics.StackTrace => 0x3f75868 => 31
	i32 67008169, ; 13: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 338
	i32 68219467, ; 14: System.Security.Cryptography.Primitives => 0x410f24b => 125
	i32 72070932, ; 15: Microsoft.Maui.Graphics.dll => 0x44bb714 => 204
	i32 82292897, ; 16: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 103
	i32 83839681, ; 17: ms\Microsoft.Maui.Controls.resources.dll => 0x4ff4ac1 => 322
	i32 101534019, ; 18: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 281
	i32 117431740, ; 19: System.Runtime.InteropServices => 0x6ffddbc => 108
	i32 120558881, ; 20: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 281
	i32 122350210, ; 21: System.Threading.Channels.dll => 0x74aea82 => 140
	i32 134690465, ; 22: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 301
	i32 136584136, ; 23: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0x8241bc8 => 337
	i32 140062828, ; 24: sk\Microsoft.Maui.Controls.resources.dll => 0x859306c => 330
	i32 142721839, ; 25: System.Net.WebHeaderCollection => 0x881c32f => 78
	i32 149972175, ; 26: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 125
	i32 159306688, ; 27: System.ComponentModel.Annotations => 0x97ed3c0 => 14
	i32 165246403, ; 28: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 237
	i32 176265551, ; 29: System.ServiceProcess => 0xa81994f => 133
	i32 182336117, ; 30: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 283
	i32 184328833, ; 31: System.ValueTuple.dll => 0xafca281 => 152
	i32 205061960, ; 32: System.ComponentModel => 0xc38ff48 => 19
	i32 209399409, ; 33: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 235
	i32 220171995, ; 34: System.Diagnostics.Debug => 0xd1f8edb => 27
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 257
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 2
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 60
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 43
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 92
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 240
	i32 276479776, ; 41: System.Threading.Timer.dll => 0x107abf20 => 148
	i32 278686392, ; 42: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 259
	i32 280482487, ; 43: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 256
	i32 291076382, ; 44: System.IO.Pipes.AccessControl.dll => 0x1159791e => 55
	i32 298918909, ; 45: System.Net.Ping.dll => 0x11d123fd => 70
	i32 317674968, ; 46: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 335
	i32 318968648, ; 47: Xamarin.AndroidX.Activity.dll => 0x13031348 => 226
	i32 321597661, ; 48: System.Numerics => 0x132b30dd => 84
	i32 321963286, ; 49: fr\Microsoft.Maui.Controls.resources.dll => 0x1330c516 => 313
	i32 329962767, ; 50: Microsoft.Practices.ServiceLocation.dll => 0x13aad50f => 174
	i32 342366114, ; 51: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 258
	i32 360082299, ; 52: System.ServiceModel.Web => 0x15766b7b => 132
	i32 364942007, ; 53: SkiaSharp.Extended.UI => 0x15c092b7 => 213
	i32 367780167, ; 54: System.IO.Pipes => 0x15ebe147 => 56
	i32 374914964, ; 55: System.Transactions.Local => 0x1658bf94 => 150
	i32 375677976, ; 56: System.Net.ServicePoint.dll => 0x16646418 => 75
	i32 379916513, ; 57: System.Threading.Thread.dll => 0x16a510e1 => 146
	i32 382590210, ; 58: SkiaSharp.Extended.dll => 0x16cddd02 => 212
	i32 385762202, ; 59: System.Memory.dll => 0x16fe439a => 63
	i32 392610295, ; 60: System.Threading.ThreadPool.dll => 0x1766c1f7 => 147
	i32 395744057, ; 61: _Microsoft.Android.Resource.Designer => 0x17969339 => 340
	i32 398680804, ; 62: Serilog.Sinks.Console => 0x17c362e4 => 209
	i32 403441872, ; 63: WindowsBase => 0x180c08d0 => 166
	i32 409257351, ; 64: tr\Microsoft.Maui.Controls.resources.dll => 0x1864c587 => 333
	i32 441335492, ; 65: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 241
	i32 442565967, ; 66: System.Collections => 0x1a61054f => 13
	i32 450948140, ; 67: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 254
	i32 451504562, ; 68: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 126
	i32 456227837, ; 69: System.Web.HttpUtility.dll => 0x1b317bfd => 153
	i32 459347974, ; 70: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 114
	i32 465846621, ; 71: mscorlib => 0x1bc4415d => 167
	i32 469710990, ; 72: System.dll => 0x1bff388e => 165
	i32 476646585, ; 73: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 256
	i32 486930444, ; 74: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 269
	i32 489220957, ; 75: es\Microsoft.Maui.Controls.resources.dll => 0x1d28eb5d => 311
	i32 498788369, ; 76: System.ObjectModel => 0x1dbae811 => 85
	i32 504833739, ; 77: SkiaSharp.SceneGraph => 0x1e1726cb => 215
	i32 513247710, ; 78: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 198
	i32 525008092, ; 79: SkiaSharp.dll => 0x1f4afcdc => 211
	i32 526420162, ; 80: System.Transactions.dll => 0x1f6088c2 => 151
	i32 527452488, ; 81: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 301
	i32 530272170, ; 82: System.Linq.Queryable => 0x1f9b4faa => 61
	i32 538707440, ; 83: th\Microsoft.Maui.Controls.resources.dll => 0x201c05f0 => 332
	i32 539058512, ; 84: Microsoft.Extensions.Logging => 0x20216150 => 194
	i32 540030774, ; 85: System.IO.FileSystem.dll => 0x20303736 => 52
	i32 545304856, ; 86: System.Runtime.Extensions => 0x2080b118 => 104
	i32 546455878, ; 87: System.Runtime.Serialization.Xml => 0x20924146 => 115
	i32 549171840, ; 88: System.Globalization.Calendars => 0x20bbb280 => 41
	i32 557405415, ; 89: Jsr305Binding => 0x213954e7 => 294
	i32 569601784, ; 90: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 292
	i32 577335427, ; 91: System.Security.Cryptography.Cng => 0x22697083 => 121
	i32 597488923, ; 92: CommunityToolkit.Maui => 0x239cf51b => 175
	i32 601371474, ; 93: System.IO.IsolatedStorage.dll => 0x23d83352 => 53
	i32 605376203, ; 94: System.IO.Compression.FileSystem => 0x24154ecb => 45
	i32 613668793, ; 95: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 120
	i32 627609679, ; 96: Xamarin.AndroidX.CustomView => 0x2568904f => 246
	i32 627931235, ; 97: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 324
	i32 639843206, ; 98: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 252
	i32 643868501, ; 99: System.Net => 0x2660a755 => 82
	i32 662205335, ; 100: System.Text.Encodings.Web.dll => 0x27787397 => 137
	i32 663517072, ; 101: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 288
	i32 666292255, ; 102: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 233
	i32 672442732, ; 103: System.Collections.Concurrent => 0x2814a96c => 9
	i32 683518922, ; 104: System.Net.Security => 0x28bdabca => 74
	i32 690569205, ; 105: System.Xml.Linq.dll => 0x29293ff5 => 156
	i32 690602616, ; 106: SkiaSharp.Skottie.dll => 0x2929c278 => 216
	i32 691348768, ; 107: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 303
	i32 693804605, ; 108: System.Windows => 0x295a9e3d => 155
	i32 699345723, ; 109: System.Reflection.Emit => 0x29af2b3b => 93
	i32 700284507, ; 110: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 298
	i32 700358131, ; 111: System.IO.Compression.ZipFile => 0x29be9df3 => 46
	i32 720511267, ; 112: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 302
	i32 722857257, ; 113: System.Runtime.Loader.dll => 0x2b15ed29 => 110
	i32 735137430, ; 114: System.Security.SecureString.dll => 0x2bd14e96 => 130
	i32 736260964, ; 115: LiveChartsCore.Behaviours => 0x2be27364 => 181
	i32 738469988, ; 116: SkiaSharp.SceneGraph.dll => 0x2c042864 => 215
	i32 752232764, ; 117: System.Diagnostics.Contracts.dll => 0x2cd6293c => 26
	i32 755313932, ; 118: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 223
	i32 756127219, ; 119: ZWave => 0x2d1195f3 => 1
	i32 759454413, ; 120: System.Net.Requests => 0x2d445acd => 73
	i32 762598435, ; 121: System.IO.Pipes.dll => 0x2d745423 => 56
	i32 775507847, ; 122: System.IO.Compression => 0x2e394f87 => 47
	i32 777317022, ; 123: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 330
	i32 778756650, ; 124: SkiaSharp.HarfBuzz.dll => 0x2e6ae22a => 214
	i32 778804420, ; 125: SkiaSharp.Extended.UI.dll => 0x2e6b9cc4 => 213
	i32 789151979, ; 126: Microsoft.Extensions.Options => 0x2f0980eb => 197
	i32 790371945, ; 127: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 247
	i32 804715423, ; 128: System.Data.Common => 0x2ff6fb9f => 23
	i32 807930345, ; 129: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 261
	i32 812630446, ; 130: Serilog => 0x306fc1ae => 207
	i32 823281589, ; 131: System.Private.Uri.dll => 0x311247b5 => 87
	i32 830298997, ; 132: System.IO.Compression.Brotli => 0x317d5b75 => 44
	i32 832635846, ; 133: System.Xml.XPath.dll => 0x31a103c6 => 161
	i32 834051424, ; 134: System.Net.Quic => 0x31b69d60 => 72
	i32 843511501, ; 135: Xamarin.AndroidX.Print => 0x3246f6cd => 274
	i32 869139383, ; 136: hi\Microsoft.Maui.Controls.resources.dll => 0x33ce03b7 => 315
	i32 873119928, ; 137: Microsoft.VisualBasic => 0x340ac0b8 => 4
	i32 877678880, ; 138: System.Globalization.dll => 0x34505120 => 43
	i32 878954865, ; 139: System.Net.Http.Json => 0x3463c971 => 64
	i32 880668424, ; 140: ru\Microsoft.Maui.Controls.resources.dll => 0x347def08 => 329
	i32 904024072, ; 141: System.ComponentModel.Primitives.dll => 0x35e25008 => 17
	i32 911108515, ; 142: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 54
	i32 918734561, ; 143: pt-BR\Microsoft.Maui.Controls.resources.dll => 0x36c2c6e1 => 326
	i32 928116545, ; 144: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 297
	i32 952186615, ; 145: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 106
	i32 955402788, ; 146: Newtonsoft.Json => 0x38f24a24 => 205
	i32 956575887, ; 147: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 302
	i32 961460050, ; 148: it\Microsoft.Maui.Controls.resources.dll => 0x394eb752 => 319
	i32 966729478, ; 149: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 295
	i32 967690846, ; 150: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 258
	i32 975236339, ; 151: System.Diagnostics.Tracing => 0x3a20ecf3 => 35
	i32 975874589, ; 152: System.Xml.XDocument => 0x3a2aaa1d => 159
	i32 986514023, ; 153: System.Private.DataContractSerialization.dll => 0x3acd0267 => 86
	i32 987214855, ; 154: System.Diagnostics.Tools => 0x3ad7b407 => 33
	i32 990727110, ; 155: ro\Microsoft.Maui.Controls.resources.dll => 0x3b0d4bc6 => 328
	i32 992768348, ; 156: System.Collections.dll => 0x3b2c715c => 13
	i32 994442037, ; 157: System.IO.FileSystem => 0x3b45fb35 => 52
	i32 999186168, ; 158: Microsoft.Extensions.FileSystemGlobbing.dll => 0x3b8e5ef8 => 193
	i32 1001831731, ; 159: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 57
	i32 1012816738, ; 160: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 278
	i32 1019214401, ; 161: System.Drawing => 0x3cbffa41 => 37
	i32 1028951442, ; 162: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 190
	i32 1031528504, ; 163: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 296
	i32 1034083993, ; 164: LiveChartsCore.SkiaSharpView.Maui.dll => 0x3da2de99 => 183
	i32 1035644815, ; 165: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 231
	i32 1036536393, ; 166: System.Drawing.Primitives.dll => 0x3dc84a49 => 36
	i32 1043950537, ; 167: de\Microsoft.Maui.Controls.resources.dll => 0x3e396bc9 => 309
	i32 1044663988, ; 168: System.Linq.Expressions.dll => 0x3e444eb4 => 59
	i32 1052210849, ; 169: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 265
	i32 1067306892, ; 170: GoogleGson => 0x3f9dcf8c => 178
	i32 1082857460, ; 171: System.ComponentModel.TypeConverter => 0x408b17f4 => 18
	i32 1084122840, ; 172: Xamarin.Kotlin.StdLib => 0x409e66d8 => 299
	i32 1098259244, ; 173: System => 0x41761b2c => 165
	i32 1106973742, ; 174: Microsoft.Extensions.Configuration.FileExtensions.dll => 0x41fb142e => 187
	i32 1108272742, ; 175: sv\Microsoft.Maui.Controls.resources.dll => 0x420ee666 => 331
	i32 1117529484, ; 176: pl\Microsoft.Maui.Controls.resources.dll => 0x429c258c => 325
	i32 1118262833, ; 177: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 321
	i32 1121599056, ; 178: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 264
	i32 1127624469, ; 179: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 196
	i32 1149092582, ; 180: Xamarin.AndroidX.Window => 0x447dc2e6 => 291
	i32 1168523401, ; 181: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 327
	i32 1170634674, ; 182: System.Web.dll => 0x45c677b2 => 154
	i32 1173126369, ; 183: Microsoft.Extensions.FileProviders.Abstractions.dll => 0x45ec7ce1 => 191
	i32 1175144683, ; 184: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 287
	i32 1178241025, ; 185: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 272
	i32 1204270330, ; 186: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 233
	i32 1208641965, ; 187: System.Diagnostics.Process => 0x480a69ad => 30
	i32 1214827643, ; 188: CommunityToolkit.Mvvm => 0x4868cc7b => 177
	i32 1219128291, ; 189: System.IO.IsolatedStorage => 0x48aa6be3 => 53
	i32 1243150071, ; 190: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 292
	i32 1253011324, ; 191: Microsoft.Win32.Registry => 0x4aaf6f7c => 6
	i32 1260983243, ; 192: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 307
	i32 1264511973, ; 193: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 282
	i32 1267360935, ; 194: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 286
	i32 1273260888, ; 195: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 238
	i32 1275534314, ; 196: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 303
	i32 1278448581, ; 197: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 230
	i32 1283425954, ; 198: LiveChartsCore.SkiaSharpView => 0x4c7f86a2 => 182
	i32 1293217323, ; 199: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 249
	i32 1308624726, ; 200: hr\Microsoft.Maui.Controls.resources.dll => 0x4e000756 => 316
	i32 1309188875, ; 201: System.Private.DataContractSerialization => 0x4e08a30b => 86
	i32 1322716291, ; 202: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 291
	i32 1322857724, ; 203: Serilog.Sinks.File.dll => 0x4ed934fc => 210
	i32 1324164729, ; 204: System.Linq => 0x4eed2679 => 62
	i32 1335329327, ; 205: System.Runtime.Serialization.Json.dll => 0x4f97822f => 113
	i32 1336711579, ; 206: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x4fac999b => 336
	i32 1364015309, ; 207: System.IO => 0x514d38cd => 58
	i32 1373134921, ; 208: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 337
	i32 1376866003, ; 209: Xamarin.AndroidX.SavedState => 0x52114ed3 => 278
	i32 1379779777, ; 210: System.Resources.ResourceManager => 0x523dc4c1 => 100
	i32 1402170036, ; 211: System.Configuration.dll => 0x53936ab4 => 20
	i32 1406073936, ; 212: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 242
	i32 1408764838, ; 213: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 112
	i32 1411638395, ; 214: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 102
	i32 1422545099, ; 215: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 103
	i32 1430672901, ; 216: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 305
	i32 1434145427, ; 217: System.Runtime.Handles => 0x557b5293 => 105
	i32 1435222561, ; 218: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 295
	i32 1439761251, ; 219: System.Net.Quic.dll => 0x55d10363 => 72
	i32 1452070440, ; 220: System.Formats.Asn1.dll => 0x568cd628 => 39
	i32 1453312822, ; 221: System.Diagnostics.Tools.dll => 0x569fcb36 => 33
	i32 1457743152, ; 222: System.Runtime.Extensions.dll => 0x56e36530 => 104
	i32 1458022317, ; 223: System.Net.Security.dll => 0x56e7a7ad => 74
	i32 1461004990, ; 224: es\Microsoft.Maui.Controls.resources => 0x57152abe => 311
	i32 1461234159, ; 225: System.Collections.Immutable.dll => 0x5718a9ef => 10
	i32 1461719063, ; 226: System.Security.Cryptography.OpenSsl => 0x57201017 => 124
	i32 1462112819, ; 227: System.IO.Compression.dll => 0x57261233 => 47
	i32 1469204771, ; 228: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 232
	i32 1470490898, ; 229: Microsoft.Extensions.Primitives => 0x57a5e912 => 198
	i32 1479771757, ; 230: System.Collections.Immutable => 0x5833866d => 10
	i32 1480492111, ; 231: System.IO.Compression.Brotli.dll => 0x583e844f => 44
	i32 1487239319, ; 232: Microsoft.Win32.Primitives => 0x58a57897 => 5
	i32 1490025113, ; 233: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 279
	i32 1521091094, ; 234: Microsoft.Extensions.FileSystemGlobbing => 0x5aaa0216 => 193
	i32 1526286932, ; 235: vi\Microsoft.Maui.Controls.resources.dll => 0x5af94a54 => 335
	i32 1536373174, ; 236: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 32
	i32 1543031311, ; 237: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 139
	i32 1543355203, ; 238: System.Reflection.Emit.dll => 0x5bfdbb43 => 93
	i32 1550322496, ; 239: System.Reflection.Extensions.dll => 0x5c680b40 => 94
	i32 1555965187, ; 240: Microsoft.Practices.ServiceLocation => 0x5cbe2503 => 174
	i32 1565862583, ; 241: System.IO.FileSystem.Primitives => 0x5d552ab7 => 50
	i32 1566207040, ; 242: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 142
	i32 1573704789, ; 243: System.Runtime.Serialization.Json => 0x5dccd455 => 113
	i32 1580037396, ; 244: System.Threading.Overlapped => 0x5e2d7514 => 141
	i32 1582372066, ; 245: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 248
	i32 1592978981, ; 246: System.Runtime.Serialization.dll => 0x5ef2ee25 => 116
	i32 1597949149, ; 247: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 296
	i32 1601112923, ; 248: System.Xml.Serialization => 0x5f6f0b5b => 158
	i32 1604827217, ; 249: System.Net.WebClient => 0x5fa7b851 => 77
	i32 1618516317, ; 250: System.Net.WebSockets.Client.dll => 0x6078995d => 80
	i32 1622152042, ; 251: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 268
	i32 1622358360, ; 252: System.Dynamic.Runtime => 0x60b33958 => 38
	i32 1623212457, ; 253: SkiaSharp.Views.Maui.Controls => 0x60c041a9 => 218
	i32 1624863272, ; 254: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 290
	i32 1625558452, ; 255: Serilog.dll => 0x60e40db4 => 207
	i32 1632842087, ; 256: Microsoft.Extensions.Configuration.Json => 0x61533167 => 188
	i32 1634654947, ; 257: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 176
	i32 1635184631, ; 258: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 252
	i32 1636350590, ; 259: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 245
	i32 1639515021, ; 260: System.Net.Http.dll => 0x61b9038d => 65
	i32 1639986890, ; 261: System.Text.RegularExpressions => 0x61c036ca => 139
	i32 1641389582, ; 262: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 16
	i32 1657153582, ; 263: System.Runtime => 0x62c6282e => 117
	i32 1658241508, ; 264: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 284
	i32 1658251792, ; 265: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 293
	i32 1670060433, ; 266: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 240
	i32 1675553242, ; 267: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 49
	i32 1677501392, ; 268: System.Net.Primitives.dll => 0x63fca3d0 => 71
	i32 1678508291, ; 269: System.Net.WebSockets => 0x640c0103 => 81
	i32 1679769178, ; 270: System.Security.Cryptography => 0x641f3e5a => 127
	i32 1691477237, ; 271: System.Reflection.Metadata => 0x64d1e4f5 => 95
	i32 1696967625, ; 272: System.Security.Cryptography.Csp => 0x6525abc9 => 122
	i32 1698840827, ; 273: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 300
	i32 1701541528, ; 274: System.Diagnostics.Debug.dll => 0x656b7698 => 27
	i32 1720223769, ; 275: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 261
	i32 1724472758, ; 276: SkiaSharp.Extended => 0x66c95db6 => 212
	i32 1726116996, ; 277: System.Reflection.dll => 0x66e27484 => 98
	i32 1728033016, ; 278: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 29
	i32 1729485958, ; 279: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 236
	i32 1743415430, ; 280: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 306
	i32 1744735666, ; 281: System.Transactions.Local.dll => 0x67fe8db2 => 150
	i32 1746316138, ; 282: Mono.Android.Export => 0x6816ab6a => 170
	i32 1750313021, ; 283: Microsoft.Win32.Primitives.dll => 0x6853a83d => 5
	i32 1756568468, ; 284: CommonLib => 0x68b31b94 => 339
	i32 1758240030, ; 285: System.Resources.Reader.dll => 0x68cc9d1e => 99
	i32 1763938596, ; 286: System.Diagnostics.TraceSource.dll => 0x69239124 => 34
	i32 1765942094, ; 287: System.Reflection.Extensions => 0x6942234e => 94
	i32 1766324549, ; 288: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 283
	i32 1770582343, ; 289: Microsoft.Extensions.Logging.dll => 0x6988f147 => 194
	i32 1775715958, ; 290: ZWave.resources => 0x69d74676 => 0
	i32 1776026572, ; 291: System.Core.dll => 0x69dc03cc => 22
	i32 1777075843, ; 292: System.Globalization.Extensions.dll => 0x69ec0683 => 42
	i32 1780572499, ; 293: Mono.Android.Runtime.dll => 0x6a216153 => 171
	i32 1782862114, ; 294: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 322
	i32 1788241197, ; 295: Xamarin.AndroidX.Fragment => 0x6a96652d => 254
	i32 1793755602, ; 296: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 314
	i32 1808609942, ; 297: Xamarin.AndroidX.Loader => 0x6bcd3296 => 268
	i32 1813058853, ; 298: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 299
	i32 1813201214, ; 299: Xamarin.Google.Android.Material => 0x6c13413e => 293
	i32 1818569960, ; 300: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 273
	i32 1818787751, ; 301: Microsoft.VisualBasic.Core => 0x6c687fa7 => 3
	i32 1824175904, ; 302: System.Text.Encoding.Extensions => 0x6cbab720 => 135
	i32 1824722060, ; 303: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 112
	i32 1828688058, ; 304: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 195
	i32 1847515442, ; 305: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 223
	i32 1853025655, ; 306: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 331
	i32 1858542181, ; 307: System.Linq.Expressions => 0x6ec71a65 => 59
	i32 1870277092, ; 308: System.Reflection.Primitives => 0x6f7a29e4 => 96
	i32 1875935024, ; 309: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 313
	i32 1879696579, ; 310: System.Formats.Tar.dll => 0x7009e4c3 => 40
	i32 1885316902, ; 311: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 234
	i32 1888955245, ; 312: System.Diagnostics.Contracts => 0x70972b6d => 26
	i32 1889954781, ; 313: System.Reflection.Metadata.dll => 0x70a66bdd => 95
	i32 1893218855, ; 314: cs\Microsoft.Maui.Controls.resources.dll => 0x70d83a27 => 307
	i32 1898237753, ; 315: System.Reflection.DispatchProxy => 0x7124cf39 => 90
	i32 1900610850, ; 316: System.Resources.ResourceManager.dll => 0x71490522 => 100
	i32 1910275211, ; 317: System.Collections.NonGeneric.dll => 0x71dc7c8b => 11
	i32 1939592360, ; 318: System.Private.Xml.Linq => 0x739bd4a8 => 88
	i32 1953182387, ; 319: id\Microsoft.Maui.Controls.resources.dll => 0x746b32b3 => 318
	i32 1956758971, ; 320: System.Resources.Writer => 0x74a1c5bb => 101
	i32 1961813231, ; 321: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 280
	i32 1968388702, ; 322: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 184
	i32 1983156543, ; 323: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 300
	i32 1985761444, ; 324: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 225
	i32 2003115576, ; 325: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 310
	i32 2011961780, ; 326: System.Buffers.dll => 0x77ec19b4 => 8
	i32 2019465201, ; 327: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 265
	i32 2025094553, ; 328: XAct.Core.PCL => 0x78b47d99 => 221
	i32 2031763787, ; 329: Xamarin.Android.Glide => 0x791a414b => 222
	i32 2045470958, ; 330: System.Private.Xml => 0x79eb68ee => 89
	i32 2048278909, ; 331: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 186
	i32 2055257422, ; 332: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 260
	i32 2060060697, ; 333: System.Windows.dll => 0x7aca0819 => 155
	i32 2066184531, ; 334: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 309
	i32 2070888862, ; 335: System.Diagnostics.TraceSource => 0x7b6f419e => 34
	i32 2072397586, ; 336: Microsoft.Extensions.FileProviders.Physical => 0x7b864712 => 192
	i32 2079903147, ; 337: System.Runtime.dll => 0x7bf8cdab => 117
	i32 2090596640, ; 338: System.Numerics.Vectors => 0x7c9bf920 => 83
	i32 2127167465, ; 339: System.Console => 0x7ec9ffe9 => 21
	i32 2142473426, ; 340: System.Collections.Specialized => 0x7fb38cd2 => 12
	i32 2143790110, ; 341: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 163
	i32 2146852085, ; 342: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 4
	i32 2159891885, ; 343: Microsoft.Maui => 0x80bd55ad => 202
	i32 2169148018, ; 344: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 317
	i32 2171397733, ; 345: Serilog.Sinks.Console.dll => 0x816ce665 => 209
	i32 2181485124, ; 346: Serilog.Sinks.File => 0x8206d244 => 210
	i32 2181898931, ; 347: Microsoft.Extensions.Options.dll => 0x820d22b3 => 197
	i32 2192057212, ; 348: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 195
	i32 2193016926, ; 349: System.ObjectModel.dll => 0x82b6c85e => 85
	i32 2201107256, ; 350: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 304
	i32 2201231467, ; 351: System.Net.Http => 0x8334206b => 65
	i32 2207618523, ; 352: it\Microsoft.Maui.Controls.resources => 0x839595db => 319
	i32 2217644978, ; 353: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 287
	i32 2222056684, ; 354: System.Threading.Tasks.Parallel => 0x8471e4ec => 144
	i32 2244775296, ; 355: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 269
	i32 2252106437, ; 356: System.Xml.Serialization.dll => 0x863c6ac5 => 158
	i32 2256313426, ; 357: System.Globalization.Extensions => 0x867c9c52 => 42
	i32 2265110946, ; 358: System.Security.AccessControl.dll => 0x8702d9a2 => 118
	i32 2266799131, ; 359: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 185
	i32 2267999099, ; 360: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 224
	i32 2279755925, ; 361: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 276
	i32 2293034957, ; 362: System.ServiceModel.Web.dll => 0x88acefcd => 132
	i32 2295906218, ; 363: System.Net.Sockets => 0x88d8bfaa => 76
	i32 2298471582, ; 364: System.Net.Mail => 0x88ffe49e => 67
	i32 2303942373, ; 365: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 323
	i32 2305521784, ; 366: System.Private.CoreLib.dll => 0x896b7878 => 173
	i32 2315684594, ; 367: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 228
	i32 2320631194, ; 368: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 144
	i32 2340441535, ; 369: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 107
	i32 2344264397, ; 370: System.ValueTuple => 0x8bbaa2cd => 152
	i32 2353062107, ; 371: System.Net.Primitives => 0x8c40e0db => 71
	i32 2358249420, ; 372: Serilog.Extensions.Logging => 0x8c9007cc => 208
	i32 2364201794, ; 373: SkiaSharp.Views.Maui.Core => 0x8ceadb42 => 220
	i32 2366048013, ; 374: hu\Microsoft.Maui.Controls.resources.dll => 0x8d07070d => 317
	i32 2368005991, ; 375: System.Xml.ReaderWriter.dll => 0x8d24e767 => 157
	i32 2371007202, ; 376: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 184
	i32 2378619854, ; 377: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 122
	i32 2383496789, ; 378: System.Security.Principal.Windows.dll => 0x8e114655 => 128
	i32 2395872292, ; 379: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 318
	i32 2401565422, ; 380: System.Web.HttpUtility => 0x8f24faee => 153
	i32 2403452196, ; 381: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 251
	i32 2421380589, ; 382: System.Threading.Tasks.Dataflow => 0x905355ed => 142
	i32 2423080555, ; 383: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 238
	i32 2427813419, ; 384: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 315
	i32 2435356389, ; 385: System.Console.dll => 0x912896e5 => 21
	i32 2435904999, ; 386: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 15
	i32 2454642406, ; 387: System.Text.Encoding.dll => 0x924edee6 => 136
	i32 2458678730, ; 388: System.Net.Sockets.dll => 0x928c75ca => 76
	i32 2459001652, ; 389: System.Linq.Parallel.dll => 0x92916334 => 60
	i32 2465532216, ; 390: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 241
	i32 2471841756, ; 391: netstandard.dll => 0x93554fdc => 168
	i32 2475788418, ; 392: Java.Interop.dll => 0x93918882 => 169
	i32 2480646305, ; 393: Microsoft.Maui.Controls => 0x93dba8a1 => 200
	i32 2483903535, ; 394: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 16
	i32 2484371297, ; 395: System.Net.ServicePoint => 0x94147f61 => 75
	i32 2490993605, ; 396: System.AppContext.dll => 0x94798bc5 => 7
	i32 2501346920, ; 397: System.Data.DataSetExtensions => 0x95178668 => 24
	i32 2503351294, ; 398: ko\Microsoft.Maui.Controls.resources.dll => 0x95361bfe => 321
	i32 2505896520, ; 399: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 263
	i32 2521915375, ; 400: SkiaSharp.Views.Maui.Controls.Compatibility => 0x96515fef => 219
	i32 2522472828, ; 401: Xamarin.Android.Glide.dll => 0x9659e17c => 222
	i32 2538310050, ; 402: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 92
	i32 2550873716, ; 403: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 316
	i32 2556439392, ; 404: LiveChartsCore.SkiaSharpView.Maui => 0x98602b60 => 183
	i32 2562349572, ; 405: Microsoft.CSharp => 0x98ba5a04 => 2
	i32 2570120770, ; 406: System.Text.Encodings.Web => 0x9930ee42 => 137
	i32 2576534780, ; 407: ja\Microsoft.Maui.Controls.resources.dll => 0x9992ccfc => 320
	i32 2581783588, ; 408: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 264
	i32 2581819634, ; 409: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 286
	i32 2585220780, ; 410: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 135
	i32 2585805581, ; 411: System.Net.Ping => 0x9a20430d => 70
	i32 2589602615, ; 412: System.Threading.ThreadPool => 0x9a5a3337 => 147
	i32 2592341985, ; 413: Microsoft.Extensions.FileProviders.Abstractions => 0x9a83ffe1 => 191
	i32 2593496499, ; 414: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 325
	i32 2605712449, ; 415: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 304
	i32 2615233544, ; 416: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 255
	i32 2616218305, ; 417: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 196
	i32 2617129537, ; 418: System.Private.Xml.dll => 0x9bfe3a41 => 89
	i32 2618712057, ; 419: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 97
	i32 2620871830, ; 420: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 245
	i32 2624644809, ; 421: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 250
	i32 2625339995, ; 422: SkiaSharp.Views.Maui.Core.dll => 0x9c7b825b => 220
	i32 2626831493, ; 423: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 320
	i32 2627185994, ; 424: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 32
	i32 2627802292, ; 425: Serilog.Extensions.Logging.dll => 0x9ca114b4 => 208
	i32 2629843544, ; 426: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 46
	i32 2633051222, ; 427: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 259
	i32 2663391936, ; 428: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 224
	i32 2663698177, ; 429: System.Runtime.Loader => 0x9ec4cf01 => 110
	i32 2664396074, ; 430: System.Xml.XDocument.dll => 0x9ecf752a => 159
	i32 2665622720, ; 431: System.Drawing.Primitives => 0x9ee22cc0 => 36
	i32 2676780864, ; 432: System.Data.Common.dll => 0x9f8c6f40 => 23
	i32 2686887180, ; 433: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 115
	i32 2693849962, ; 434: System.IO.dll => 0xa090e36a => 58
	i32 2701096212, ; 435: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 284
	i32 2715334215, ; 436: System.Threading.Tasks.dll => 0xa1d8b647 => 145
	i32 2717744543, ; 437: System.Security.Claims => 0xa1fd7d9f => 119
	i32 2719963679, ; 438: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 121
	i32 2724373263, ; 439: System.Runtime.Numerics.dll => 0xa262a30f => 111
	i32 2732626843, ; 440: Xamarin.AndroidX.Activity => 0xa2e0939b => 226
	i32 2735172069, ; 441: System.Threading.Channels => 0xa30769e5 => 140
	i32 2737747696, ; 442: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 232
	i32 2740698338, ; 443: ca\Microsoft.Maui.Controls.resources.dll => 0xa35bbce2 => 306
	i32 2740948882, ; 444: System.IO.Pipes.AccessControl => 0xa35f8f92 => 55
	i32 2748088231, ; 445: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 106
	i32 2752995522, ; 446: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 326
	i32 2753700710, ; 447: CommonLib.dll => 0xa4222366 => 339
	i32 2758225723, ; 448: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 201
	i32 2764765095, ; 449: Microsoft.Maui.dll => 0xa4caf7a7 => 202
	i32 2765824710, ; 450: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 134
	i32 2770495804, ; 451: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 298
	i32 2778768386, ; 452: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 289
	i32 2779977773, ; 453: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 277
	i32 2785988530, ; 454: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 332
	i32 2788224221, ; 455: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 255
	i32 2795602088, ; 456: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 217
	i32 2801831435, ; 457: Microsoft.Maui.Graphics => 0xa7008e0b => 204
	i32 2803228030, ; 458: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 160
	i32 2810250172, ; 459: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 242
	i32 2819470561, ; 460: System.Xml.dll => 0xa80db4e1 => 164
	i32 2821205001, ; 461: System.ServiceProcess.dll => 0xa8282c09 => 133
	i32 2821294376, ; 462: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 277
	i32 2824502124, ; 463: System.Xml.XmlDocument => 0xa85a7b6c => 162
	i32 2838993487, ; 464: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 266
	i32 2849599387, ; 465: System.Threading.Overlapped.dll => 0xa9d96f9b => 141
	i32 2853208004, ; 466: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 289
	i32 2855708567, ; 467: Xamarin.AndroidX.Transition => 0xaa36a797 => 285
	i32 2861098320, ; 468: Mono.Android.Export.dll => 0xaa88e550 => 170
	i32 2861189240, ; 469: Microsoft.Maui.Essentials => 0xaa8a4878 => 203
	i32 2868488919, ; 470: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 176
	i32 2869955538, ; 471: ZWave.resources.dll => 0xab100bd2 => 0
	i32 2870099610, ; 472: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 227
	i32 2875164099, ; 473: Jsr305Binding.dll => 0xab5f85c3 => 294
	i32 2875220617, ; 474: System.Globalization.Calendars.dll => 0xab606289 => 41
	i32 2884993177, ; 475: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 253
	i32 2887636118, ; 476: System.Net.dll => 0xac1dd496 => 82
	i32 2899753641, ; 477: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 57
	i32 2900621748, ; 478: System.Dynamic.Runtime.dll => 0xace3f9b4 => 38
	i32 2901442782, ; 479: System.Reflection => 0xacf080de => 98
	i32 2905242038, ; 480: mscorlib.dll => 0xad2a79b6 => 167
	i32 2909740682, ; 481: System.Private.CoreLib => 0xad6f1e8a => 173
	i32 2911054922, ; 482: Microsoft.Extensions.FileProviders.Physical.dll => 0xad832c4a => 192
	i32 2912489636, ; 483: SkiaSharp.Views.Android => 0xad9910a4 => 217
	i32 2916838712, ; 484: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 290
	i32 2919462931, ; 485: System.Numerics.Vectors.dll => 0xae037813 => 83
	i32 2921128767, ; 486: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 229
	i32 2936416060, ; 487: System.Resources.Reader => 0xaf06273c => 99
	i32 2940926066, ; 488: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 31
	i32 2942453041, ; 489: System.Xml.XPath.XDocument => 0xaf624531 => 160
	i32 2959614098, ; 490: System.ComponentModel.dll => 0xb0682092 => 19
	i32 2968338931, ; 491: System.Security.Principal.Windows => 0xb0ed41f3 => 128
	i32 2972252294, ; 492: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 120
	i32 2978675010, ; 493: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 249
	i32 2987532451, ; 494: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 280
	i32 2996846495, ; 495: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 262
	i32 3016983068, ; 496: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 282
	i32 3023353419, ; 497: WindowsBase.dll => 0xb434b64b => 166
	i32 3024354802, ; 498: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 257
	i32 3038032645, ; 499: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 340
	i32 3053864966, ; 500: fi\Microsoft.Maui.Controls.resources.dll => 0xb6064806 => 312
	i32 3056245963, ; 501: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 279
	i32 3057625584, ; 502: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 270
	i32 3059408633, ; 503: Mono.Android.Runtime => 0xb65adef9 => 171
	i32 3059793426, ; 504: System.ComponentModel.Primitives => 0xb660be12 => 17
	i32 3075834255, ; 505: System.Threading.Tasks => 0xb755818f => 145
	i32 3081706019, ; 506: LiveChartsCore => 0xb7af1a23 => 180
	i32 3090735792, ; 507: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 126
	i32 3099732863, ; 508: System.Security.Claims.dll => 0xb8c22b7f => 119
	i32 3103600923, ; 509: System.Formats.Asn1 => 0xb8fd311b => 39
	i32 3105468037, ; 510: ZWave.dll => 0xb919ae85 => 1
	i32 3111772706, ; 511: System.Runtime.Serialization => 0xb979e222 => 116
	i32 3121463068, ; 512: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 48
	i32 3124832203, ; 513: System.Threading.Tasks.Extensions => 0xba4127cb => 143
	i32 3132293585, ; 514: System.Security.AccessControl => 0xbab301d1 => 118
	i32 3147165239, ; 515: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 35
	i32 3148237826, ; 516: GoogleGson.dll => 0xbba64c02 => 178
	i32 3159123045, ; 517: System.Reflection.Primitives.dll => 0xbc4c6465 => 96
	i32 3160747431, ; 518: System.IO.MemoryMappedFiles => 0xbc652da7 => 54
	i32 3178803400, ; 519: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 271
	i32 3192346100, ; 520: System.Security.SecureString => 0xbe4755f4 => 130
	i32 3193515020, ; 521: System.Web => 0xbe592c0c => 154
	i32 3204380047, ; 522: System.Data.dll => 0xbefef58f => 25
	i32 3209718065, ; 523: System.Xml.XmlDocument.dll => 0xbf506931 => 162
	i32 3211777861, ; 524: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 248
	i32 3220365878, ; 525: System.Threading => 0xbff2e236 => 149
	i32 3226221578, ; 526: System.Runtime.Handles.dll => 0xc04c3c0a => 105
	i32 3243190844, ; 527: XAct.Core.PCL.dll => 0xc14f2a3c => 221
	i32 3251039220, ; 528: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 90
	i32 3258312781, ; 529: Xamarin.AndroidX.CardView => 0xc235e84d => 236
	i32 3265493905, ; 530: System.Linq.Queryable.dll => 0xc2a37b91 => 61
	i32 3265893370, ; 531: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 143
	i32 3277815716, ; 532: System.Resources.Writer.dll => 0xc35f7fa4 => 101
	i32 3279906254, ; 533: Microsoft.Win32.Registry.dll => 0xc37f65ce => 6
	i32 3280506390, ; 534: System.ComponentModel.Annotations.dll => 0xc3888e16 => 14
	i32 3290767353, ; 535: System.Security.Cryptography.Encoding => 0xc4251ff9 => 123
	i32 3299363146, ; 536: System.Text.Encoding => 0xc4a8494a => 136
	i32 3303498502, ; 537: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 29
	i32 3305363605, ; 538: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 312
	i32 3316684772, ; 539: System.Net.Requests.dll => 0xc5b097e4 => 73
	i32 3317135071, ; 540: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 246
	i32 3317144872, ; 541: System.Data => 0xc5b79d28 => 25
	i32 3340387945, ; 542: SkiaSharp => 0xc71a4669 => 211
	i32 3340431453, ; 543: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 234
	i32 3345895724, ; 544: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 275
	i32 3346324047, ; 545: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 272
	i32 3357674450, ; 546: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 329
	i32 3358260929, ; 547: System.Text.Json => 0xc82afec1 => 138
	i32 3362336904, ; 548: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 227
	i32 3362522851, ; 549: Xamarin.AndroidX.Core => 0xc86c06e3 => 243
	i32 3366347497, ; 550: Java.Interop => 0xc8a662e9 => 169
	i32 3374999561, ; 551: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 276
	i32 3381016424, ; 552: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 308
	i32 3384551493, ; 553: LiveChartsCore.dll => 0xc9bc2845 => 180
	i32 3395150330, ; 554: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 102
	i32 3403906625, ; 555: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 124
	i32 3405233483, ; 556: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 247
	i32 3421170118, ; 557: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 186
	i32 3428513518, ; 558: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 189
	i32 3429136800, ; 559: System.Xml => 0xcc6479a0 => 164
	i32 3430777524, ; 560: netstandard => 0xcc7d82b4 => 168
	i32 3441283291, ; 561: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 250
	i32 3445260447, ; 562: System.Formats.Tar => 0xcd5a809f => 40
	i32 3452344032, ; 563: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 199
	i32 3458724246, ; 564: pt\Microsoft.Maui.Controls.resources.dll => 0xce27f196 => 327
	i32 3466574376, ; 565: SkiaSharp.Views.Maui.Controls.Compatibility.dll => 0xce9fba28 => 219
	i32 3471940407, ; 566: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 18
	i32 3473156932, ; 567: SkiaSharp.Views.Maui.Controls.dll => 0xcf042b44 => 218
	i32 3476120550, ; 568: Mono.Android => 0xcf3163e6 => 172
	i32 3484440000, ; 569: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 328
	i32 3485117614, ; 570: System.Text.Json.dll => 0xcfbaacae => 138
	i32 3486566296, ; 571: System.Transactions => 0xcfd0c798 => 151
	i32 3493954962, ; 572: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 239
	i32 3509114376, ; 573: System.Xml.Linq => 0xd128d608 => 156
	i32 3515174580, ; 574: System.Security.dll => 0xd1854eb4 => 131
	i32 3530912306, ; 575: System.Configuration => 0xd2757232 => 20
	i32 3539954161, ; 576: System.Net.HttpListener => 0xd2ff69f1 => 66
	i32 3556829416, ; 577: LiveChartsCore.Behaviours.dll => 0xd400e8e8 => 181
	i32 3560100363, ; 578: System.Threading.Timer => 0xd432d20b => 148
	i32 3570554715, ; 579: System.IO.FileSystem.AccessControl => 0xd4d2575b => 48
	i32 3580758918, ; 580: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 336
	i32 3592228221, ; 581: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0xd61d0d7d => 338
	i32 3597029428, ; 582: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 225
	i32 3598340787, ; 583: System.Net.WebSockets.Client => 0xd67a52b3 => 80
	i32 3608519521, ; 584: System.Linq.dll => 0xd715a361 => 62
	i32 3624195450, ; 585: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 107
	i32 3627220390, ; 586: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 274
	i32 3633644679, ; 587: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 229
	i32 3638274909, ; 588: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 50
	i32 3641597786, ; 589: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 260
	i32 3643446276, ; 590: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 333
	i32 3643854240, ; 591: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 271
	i32 3645089577, ; 592: System.ComponentModel.DataAnnotations => 0xd943a729 => 15
	i32 3657292374, ; 593: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 185
	i32 3660523487, ; 594: System.Net.NetworkInformation => 0xda2f27df => 69
	i32 3663323240, ; 595: SkiaSharp.Skottie => 0xda59e068 => 216
	i32 3672681054, ; 596: Mono.Android.dll => 0xdae8aa5e => 172
	i32 3682565725, ; 597: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 235
	i32 3684561358, ; 598: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 239
	i32 3700866549, ; 599: System.Net.WebProxy.dll => 0xdc96bdf5 => 79
	i32 3706696989, ; 600: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 244
	i32 3707723043, ; 601: RabbitMQ.Client => 0xdcff5d23 => 206
	i32 3716563718, ; 602: System.Runtime.Intrinsics => 0xdd864306 => 109
	i32 3718780102, ; 603: Xamarin.AndroidX.Annotation => 0xdda814c6 => 228
	i32 3722202641, ; 604: Microsoft.Extensions.Configuration.Json.dll => 0xdddc4e11 => 188
	i32 3724971120, ; 605: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 270
	i32 3732100267, ; 606: System.Net.NameResolution => 0xde7354ab => 68
	i32 3737834244, ; 607: System.Net.Http.Json.dll => 0xdecad304 => 64
	i32 3748608112, ; 608: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 28
	i32 3751444290, ; 609: System.Xml.XPath => 0xdf9a7f42 => 161
	i32 3751619990, ; 610: da\Microsoft.Maui.Controls.resources.dll => 0xdf9d2d96 => 308
	i32 3758424670, ; 611: Microsoft.Extensions.Configuration.FileExtensions => 0xe005025e => 187
	i32 3786282454, ; 612: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 237
	i32 3792276235, ; 613: System.Collections.NonGeneric => 0xe2098b0b => 11
	i32 3792835768, ; 614: HarfBuzzSharp => 0xe21214b8 => 179
	i32 3800979733, ; 615: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 199
	i32 3802395368, ; 616: System.Collections.Specialized.dll => 0xe2a3f2e8 => 12
	i32 3817368567, ; 617: CommunityToolkit.Maui.dll => 0xe3886bf7 => 175
	i32 3819260425, ; 618: System.Net.WebProxy => 0xe3a54a09 => 79
	i32 3823082795, ; 619: System.Security.Cryptography.dll => 0xe3df9d2b => 127
	i32 3829621856, ; 620: System.Numerics.dll => 0xe4436460 => 84
	i32 3841636137, ; 621: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 190
	i32 3844307129, ; 622: System.Net.Mail.dll => 0xe52378b9 => 67
	i32 3849253459, ; 623: System.Runtime.InteropServices.dll => 0xe56ef253 => 108
	i32 3870376305, ; 624: System.Net.HttpListener.dll => 0xe6b14171 => 66
	i32 3873536506, ; 625: System.Security.Principal => 0xe6e179fa => 129
	i32 3875112723, ; 626: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 123
	i32 3885497537, ; 627: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 78
	i32 3885922214, ; 628: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 285
	i32 3888767677, ; 629: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 275
	i32 3896106733, ; 630: System.Collections.Concurrent.dll => 0xe839deed => 9
	i32 3896760992, ; 631: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 243
	i32 3901907137, ; 632: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 3
	i32 3920221145, ; 633: nl\Microsoft.Maui.Controls.resources.dll => 0xe9a9d3d9 => 324
	i32 3920810846, ; 634: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 45
	i32 3921031405, ; 635: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 288
	i32 3928044579, ; 636: System.Xml.ReaderWriter => 0xea213423 => 157
	i32 3930554604, ; 637: System.Security.Principal.dll => 0xea4780ec => 129
	i32 3931092270, ; 638: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 273
	i32 3945713374, ; 639: System.Data.DataSetExtensions.dll => 0xeb2ecede => 24
	i32 3953953790, ; 640: System.Text.Encoding.CodePages => 0xebac8bfe => 134
	i32 3955647286, ; 641: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 231
	i32 3959773229, ; 642: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 262
	i32 4003436829, ; 643: System.Diagnostics.Process.dll => 0xee9f991d => 30
	i32 4003906742, ; 644: HarfBuzzSharp.dll => 0xeea6c4b6 => 179
	i32 4015948917, ; 645: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 230
	i32 4025784931, ; 646: System.Memory => 0xeff49a63 => 63
	i32 4046471985, ; 647: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 201
	i32 4054681211, ; 648: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 91
	i32 4066802364, ; 649: SkiaSharp.HarfBuzz => 0xf2667abc => 214
	i32 4068434129, ; 650: System.Private.Xml.Linq.dll => 0xf27f60d1 => 88
	i32 4073602200, ; 651: System.Threading.dll => 0xf2ce3c98 => 149
	i32 4091086043, ; 652: el\Microsoft.Maui.Controls.resources.dll => 0xf3d904db => 310
	i32 4094352644, ; 653: Microsoft.Maui.Essentials.dll => 0xf40add04 => 203
	i32 4099507663, ; 654: System.Drawing.dll => 0xf45985cf => 37
	i32 4100113165, ; 655: System.Private.Uri => 0xf462c30d => 87
	i32 4101593132, ; 656: Xamarin.AndroidX.Emoji2 => 0xf479582c => 251
	i32 4103439459, ; 657: uk\Microsoft.Maui.Controls.resources.dll => 0xf4958463 => 334
	i32 4126470640, ; 658: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 189
	i32 4127667938, ; 659: System.IO.FileSystem.Watcher => 0xf60736e2 => 51
	i32 4130442656, ; 660: System.AppContext => 0xf6318da0 => 7
	i32 4147896353, ; 661: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 91
	i32 4150914736, ; 662: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 334
	i32 4151237749, ; 663: System.Core => 0xf76edc75 => 22
	i32 4159265925, ; 664: System.Xml.XmlSerializer => 0xf7e95c85 => 163
	i32 4161255271, ; 665: System.Reflection.TypeExtensions => 0xf807b767 => 97
	i32 4164802419, ; 666: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 51
	i32 4181436372, ; 667: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 114
	i32 4182413190, ; 668: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 267
	i32 4185676441, ; 669: System.Security => 0xf97c5a99 => 131
	i32 4196529839, ; 670: System.Net.WebClient.dll => 0xfa21f6af => 77
	i32 4213026141, ; 671: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 28
	i32 4240444043, ; 672: RabbitMQ.Client.dll => 0xfcc00a8b => 206
	i32 4249188766, ; 673: nb\Microsoft.Maui.Controls.resources.dll => 0xfd45799e => 323
	i32 4256097574, ; 674: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 244
	i32 4258378803, ; 675: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 266
	i32 4260525087, ; 676: System.Buffers => 0xfdf2741f => 8
	i32 4271975918, ; 677: Microsoft.Maui.Controls.dll => 0xfea12dee => 200
	i32 4274623895, ; 678: CommunityToolkit.Mvvm.dll => 0xfec99597 => 177
	i32 4274976490, ; 679: System.Runtime.Numerics => 0xfecef6ea => 111
	i32 4292120959, ; 680: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 267
	i32 4294763496 ; 681: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 253
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [682 x i32] [
	i32 69, ; 0
	i32 68, ; 1
	i32 182, ; 2
	i32 109, ; 3
	i32 263, ; 4
	i32 297, ; 5
	i32 49, ; 6
	i32 305, ; 7
	i32 205, ; 8
	i32 81, ; 9
	i32 314, ; 10
	i32 146, ; 11
	i32 31, ; 12
	i32 338, ; 13
	i32 125, ; 14
	i32 204, ; 15
	i32 103, ; 16
	i32 322, ; 17
	i32 281, ; 18
	i32 108, ; 19
	i32 281, ; 20
	i32 140, ; 21
	i32 301, ; 22
	i32 337, ; 23
	i32 330, ; 24
	i32 78, ; 25
	i32 125, ; 26
	i32 14, ; 27
	i32 237, ; 28
	i32 133, ; 29
	i32 283, ; 30
	i32 152, ; 31
	i32 19, ; 32
	i32 235, ; 33
	i32 27, ; 34
	i32 257, ; 35
	i32 2, ; 36
	i32 60, ; 37
	i32 43, ; 38
	i32 92, ; 39
	i32 240, ; 40
	i32 148, ; 41
	i32 259, ; 42
	i32 256, ; 43
	i32 55, ; 44
	i32 70, ; 45
	i32 335, ; 46
	i32 226, ; 47
	i32 84, ; 48
	i32 313, ; 49
	i32 174, ; 50
	i32 258, ; 51
	i32 132, ; 52
	i32 213, ; 53
	i32 56, ; 54
	i32 150, ; 55
	i32 75, ; 56
	i32 146, ; 57
	i32 212, ; 58
	i32 63, ; 59
	i32 147, ; 60
	i32 340, ; 61
	i32 209, ; 62
	i32 166, ; 63
	i32 333, ; 64
	i32 241, ; 65
	i32 13, ; 66
	i32 254, ; 67
	i32 126, ; 68
	i32 153, ; 69
	i32 114, ; 70
	i32 167, ; 71
	i32 165, ; 72
	i32 256, ; 73
	i32 269, ; 74
	i32 311, ; 75
	i32 85, ; 76
	i32 215, ; 77
	i32 198, ; 78
	i32 211, ; 79
	i32 151, ; 80
	i32 301, ; 81
	i32 61, ; 82
	i32 332, ; 83
	i32 194, ; 84
	i32 52, ; 85
	i32 104, ; 86
	i32 115, ; 87
	i32 41, ; 88
	i32 294, ; 89
	i32 292, ; 90
	i32 121, ; 91
	i32 175, ; 92
	i32 53, ; 93
	i32 45, ; 94
	i32 120, ; 95
	i32 246, ; 96
	i32 324, ; 97
	i32 252, ; 98
	i32 82, ; 99
	i32 137, ; 100
	i32 288, ; 101
	i32 233, ; 102
	i32 9, ; 103
	i32 74, ; 104
	i32 156, ; 105
	i32 216, ; 106
	i32 303, ; 107
	i32 155, ; 108
	i32 93, ; 109
	i32 298, ; 110
	i32 46, ; 111
	i32 302, ; 112
	i32 110, ; 113
	i32 130, ; 114
	i32 181, ; 115
	i32 215, ; 116
	i32 26, ; 117
	i32 223, ; 118
	i32 1, ; 119
	i32 73, ; 120
	i32 56, ; 121
	i32 47, ; 122
	i32 330, ; 123
	i32 214, ; 124
	i32 213, ; 125
	i32 197, ; 126
	i32 247, ; 127
	i32 23, ; 128
	i32 261, ; 129
	i32 207, ; 130
	i32 87, ; 131
	i32 44, ; 132
	i32 161, ; 133
	i32 72, ; 134
	i32 274, ; 135
	i32 315, ; 136
	i32 4, ; 137
	i32 43, ; 138
	i32 64, ; 139
	i32 329, ; 140
	i32 17, ; 141
	i32 54, ; 142
	i32 326, ; 143
	i32 297, ; 144
	i32 106, ; 145
	i32 205, ; 146
	i32 302, ; 147
	i32 319, ; 148
	i32 295, ; 149
	i32 258, ; 150
	i32 35, ; 151
	i32 159, ; 152
	i32 86, ; 153
	i32 33, ; 154
	i32 328, ; 155
	i32 13, ; 156
	i32 52, ; 157
	i32 193, ; 158
	i32 57, ; 159
	i32 278, ; 160
	i32 37, ; 161
	i32 190, ; 162
	i32 296, ; 163
	i32 183, ; 164
	i32 231, ; 165
	i32 36, ; 166
	i32 309, ; 167
	i32 59, ; 168
	i32 265, ; 169
	i32 178, ; 170
	i32 18, ; 171
	i32 299, ; 172
	i32 165, ; 173
	i32 187, ; 174
	i32 331, ; 175
	i32 325, ; 176
	i32 321, ; 177
	i32 264, ; 178
	i32 196, ; 179
	i32 291, ; 180
	i32 327, ; 181
	i32 154, ; 182
	i32 191, ; 183
	i32 287, ; 184
	i32 272, ; 185
	i32 233, ; 186
	i32 30, ; 187
	i32 177, ; 188
	i32 53, ; 189
	i32 292, ; 190
	i32 6, ; 191
	i32 307, ; 192
	i32 282, ; 193
	i32 286, ; 194
	i32 238, ; 195
	i32 303, ; 196
	i32 230, ; 197
	i32 182, ; 198
	i32 249, ; 199
	i32 316, ; 200
	i32 86, ; 201
	i32 291, ; 202
	i32 210, ; 203
	i32 62, ; 204
	i32 113, ; 205
	i32 336, ; 206
	i32 58, ; 207
	i32 337, ; 208
	i32 278, ; 209
	i32 100, ; 210
	i32 20, ; 211
	i32 242, ; 212
	i32 112, ; 213
	i32 102, ; 214
	i32 103, ; 215
	i32 305, ; 216
	i32 105, ; 217
	i32 295, ; 218
	i32 72, ; 219
	i32 39, ; 220
	i32 33, ; 221
	i32 104, ; 222
	i32 74, ; 223
	i32 311, ; 224
	i32 10, ; 225
	i32 124, ; 226
	i32 47, ; 227
	i32 232, ; 228
	i32 198, ; 229
	i32 10, ; 230
	i32 44, ; 231
	i32 5, ; 232
	i32 279, ; 233
	i32 193, ; 234
	i32 335, ; 235
	i32 32, ; 236
	i32 139, ; 237
	i32 93, ; 238
	i32 94, ; 239
	i32 174, ; 240
	i32 50, ; 241
	i32 142, ; 242
	i32 113, ; 243
	i32 141, ; 244
	i32 248, ; 245
	i32 116, ; 246
	i32 296, ; 247
	i32 158, ; 248
	i32 77, ; 249
	i32 80, ; 250
	i32 268, ; 251
	i32 38, ; 252
	i32 218, ; 253
	i32 290, ; 254
	i32 207, ; 255
	i32 188, ; 256
	i32 176, ; 257
	i32 252, ; 258
	i32 245, ; 259
	i32 65, ; 260
	i32 139, ; 261
	i32 16, ; 262
	i32 117, ; 263
	i32 284, ; 264
	i32 293, ; 265
	i32 240, ; 266
	i32 49, ; 267
	i32 71, ; 268
	i32 81, ; 269
	i32 127, ; 270
	i32 95, ; 271
	i32 122, ; 272
	i32 300, ; 273
	i32 27, ; 274
	i32 261, ; 275
	i32 212, ; 276
	i32 98, ; 277
	i32 29, ; 278
	i32 236, ; 279
	i32 306, ; 280
	i32 150, ; 281
	i32 170, ; 282
	i32 5, ; 283
	i32 339, ; 284
	i32 99, ; 285
	i32 34, ; 286
	i32 94, ; 287
	i32 283, ; 288
	i32 194, ; 289
	i32 0, ; 290
	i32 22, ; 291
	i32 42, ; 292
	i32 171, ; 293
	i32 322, ; 294
	i32 254, ; 295
	i32 314, ; 296
	i32 268, ; 297
	i32 299, ; 298
	i32 293, ; 299
	i32 273, ; 300
	i32 3, ; 301
	i32 135, ; 302
	i32 112, ; 303
	i32 195, ; 304
	i32 223, ; 305
	i32 331, ; 306
	i32 59, ; 307
	i32 96, ; 308
	i32 313, ; 309
	i32 40, ; 310
	i32 234, ; 311
	i32 26, ; 312
	i32 95, ; 313
	i32 307, ; 314
	i32 90, ; 315
	i32 100, ; 316
	i32 11, ; 317
	i32 88, ; 318
	i32 318, ; 319
	i32 101, ; 320
	i32 280, ; 321
	i32 184, ; 322
	i32 300, ; 323
	i32 225, ; 324
	i32 310, ; 325
	i32 8, ; 326
	i32 265, ; 327
	i32 221, ; 328
	i32 222, ; 329
	i32 89, ; 330
	i32 186, ; 331
	i32 260, ; 332
	i32 155, ; 333
	i32 309, ; 334
	i32 34, ; 335
	i32 192, ; 336
	i32 117, ; 337
	i32 83, ; 338
	i32 21, ; 339
	i32 12, ; 340
	i32 163, ; 341
	i32 4, ; 342
	i32 202, ; 343
	i32 317, ; 344
	i32 209, ; 345
	i32 210, ; 346
	i32 197, ; 347
	i32 195, ; 348
	i32 85, ; 349
	i32 304, ; 350
	i32 65, ; 351
	i32 319, ; 352
	i32 287, ; 353
	i32 144, ; 354
	i32 269, ; 355
	i32 158, ; 356
	i32 42, ; 357
	i32 118, ; 358
	i32 185, ; 359
	i32 224, ; 360
	i32 276, ; 361
	i32 132, ; 362
	i32 76, ; 363
	i32 67, ; 364
	i32 323, ; 365
	i32 173, ; 366
	i32 228, ; 367
	i32 144, ; 368
	i32 107, ; 369
	i32 152, ; 370
	i32 71, ; 371
	i32 208, ; 372
	i32 220, ; 373
	i32 317, ; 374
	i32 157, ; 375
	i32 184, ; 376
	i32 122, ; 377
	i32 128, ; 378
	i32 318, ; 379
	i32 153, ; 380
	i32 251, ; 381
	i32 142, ; 382
	i32 238, ; 383
	i32 315, ; 384
	i32 21, ; 385
	i32 15, ; 386
	i32 136, ; 387
	i32 76, ; 388
	i32 60, ; 389
	i32 241, ; 390
	i32 168, ; 391
	i32 169, ; 392
	i32 200, ; 393
	i32 16, ; 394
	i32 75, ; 395
	i32 7, ; 396
	i32 24, ; 397
	i32 321, ; 398
	i32 263, ; 399
	i32 219, ; 400
	i32 222, ; 401
	i32 92, ; 402
	i32 316, ; 403
	i32 183, ; 404
	i32 2, ; 405
	i32 137, ; 406
	i32 320, ; 407
	i32 264, ; 408
	i32 286, ; 409
	i32 135, ; 410
	i32 70, ; 411
	i32 147, ; 412
	i32 191, ; 413
	i32 325, ; 414
	i32 304, ; 415
	i32 255, ; 416
	i32 196, ; 417
	i32 89, ; 418
	i32 97, ; 419
	i32 245, ; 420
	i32 250, ; 421
	i32 220, ; 422
	i32 320, ; 423
	i32 32, ; 424
	i32 208, ; 425
	i32 46, ; 426
	i32 259, ; 427
	i32 224, ; 428
	i32 110, ; 429
	i32 159, ; 430
	i32 36, ; 431
	i32 23, ; 432
	i32 115, ; 433
	i32 58, ; 434
	i32 284, ; 435
	i32 145, ; 436
	i32 119, ; 437
	i32 121, ; 438
	i32 111, ; 439
	i32 226, ; 440
	i32 140, ; 441
	i32 232, ; 442
	i32 306, ; 443
	i32 55, ; 444
	i32 106, ; 445
	i32 326, ; 446
	i32 339, ; 447
	i32 201, ; 448
	i32 202, ; 449
	i32 134, ; 450
	i32 298, ; 451
	i32 289, ; 452
	i32 277, ; 453
	i32 332, ; 454
	i32 255, ; 455
	i32 217, ; 456
	i32 204, ; 457
	i32 160, ; 458
	i32 242, ; 459
	i32 164, ; 460
	i32 133, ; 461
	i32 277, ; 462
	i32 162, ; 463
	i32 266, ; 464
	i32 141, ; 465
	i32 289, ; 466
	i32 285, ; 467
	i32 170, ; 468
	i32 203, ; 469
	i32 176, ; 470
	i32 0, ; 471
	i32 227, ; 472
	i32 294, ; 473
	i32 41, ; 474
	i32 253, ; 475
	i32 82, ; 476
	i32 57, ; 477
	i32 38, ; 478
	i32 98, ; 479
	i32 167, ; 480
	i32 173, ; 481
	i32 192, ; 482
	i32 217, ; 483
	i32 290, ; 484
	i32 83, ; 485
	i32 229, ; 486
	i32 99, ; 487
	i32 31, ; 488
	i32 160, ; 489
	i32 19, ; 490
	i32 128, ; 491
	i32 120, ; 492
	i32 249, ; 493
	i32 280, ; 494
	i32 262, ; 495
	i32 282, ; 496
	i32 166, ; 497
	i32 257, ; 498
	i32 340, ; 499
	i32 312, ; 500
	i32 279, ; 501
	i32 270, ; 502
	i32 171, ; 503
	i32 17, ; 504
	i32 145, ; 505
	i32 180, ; 506
	i32 126, ; 507
	i32 119, ; 508
	i32 39, ; 509
	i32 1, ; 510
	i32 116, ; 511
	i32 48, ; 512
	i32 143, ; 513
	i32 118, ; 514
	i32 35, ; 515
	i32 178, ; 516
	i32 96, ; 517
	i32 54, ; 518
	i32 271, ; 519
	i32 130, ; 520
	i32 154, ; 521
	i32 25, ; 522
	i32 162, ; 523
	i32 248, ; 524
	i32 149, ; 525
	i32 105, ; 526
	i32 221, ; 527
	i32 90, ; 528
	i32 236, ; 529
	i32 61, ; 530
	i32 143, ; 531
	i32 101, ; 532
	i32 6, ; 533
	i32 14, ; 534
	i32 123, ; 535
	i32 136, ; 536
	i32 29, ; 537
	i32 312, ; 538
	i32 73, ; 539
	i32 246, ; 540
	i32 25, ; 541
	i32 211, ; 542
	i32 234, ; 543
	i32 275, ; 544
	i32 272, ; 545
	i32 329, ; 546
	i32 138, ; 547
	i32 227, ; 548
	i32 243, ; 549
	i32 169, ; 550
	i32 276, ; 551
	i32 308, ; 552
	i32 180, ; 553
	i32 102, ; 554
	i32 124, ; 555
	i32 247, ; 556
	i32 186, ; 557
	i32 189, ; 558
	i32 164, ; 559
	i32 168, ; 560
	i32 250, ; 561
	i32 40, ; 562
	i32 199, ; 563
	i32 327, ; 564
	i32 219, ; 565
	i32 18, ; 566
	i32 218, ; 567
	i32 172, ; 568
	i32 328, ; 569
	i32 138, ; 570
	i32 151, ; 571
	i32 239, ; 572
	i32 156, ; 573
	i32 131, ; 574
	i32 20, ; 575
	i32 66, ; 576
	i32 181, ; 577
	i32 148, ; 578
	i32 48, ; 579
	i32 336, ; 580
	i32 338, ; 581
	i32 225, ; 582
	i32 80, ; 583
	i32 62, ; 584
	i32 107, ; 585
	i32 274, ; 586
	i32 229, ; 587
	i32 50, ; 588
	i32 260, ; 589
	i32 333, ; 590
	i32 271, ; 591
	i32 15, ; 592
	i32 185, ; 593
	i32 69, ; 594
	i32 216, ; 595
	i32 172, ; 596
	i32 235, ; 597
	i32 239, ; 598
	i32 79, ; 599
	i32 244, ; 600
	i32 206, ; 601
	i32 109, ; 602
	i32 228, ; 603
	i32 188, ; 604
	i32 270, ; 605
	i32 68, ; 606
	i32 64, ; 607
	i32 28, ; 608
	i32 161, ; 609
	i32 308, ; 610
	i32 187, ; 611
	i32 237, ; 612
	i32 11, ; 613
	i32 179, ; 614
	i32 199, ; 615
	i32 12, ; 616
	i32 175, ; 617
	i32 79, ; 618
	i32 127, ; 619
	i32 84, ; 620
	i32 190, ; 621
	i32 67, ; 622
	i32 108, ; 623
	i32 66, ; 624
	i32 129, ; 625
	i32 123, ; 626
	i32 78, ; 627
	i32 285, ; 628
	i32 275, ; 629
	i32 9, ; 630
	i32 243, ; 631
	i32 3, ; 632
	i32 324, ; 633
	i32 45, ; 634
	i32 288, ; 635
	i32 157, ; 636
	i32 129, ; 637
	i32 273, ; 638
	i32 24, ; 639
	i32 134, ; 640
	i32 231, ; 641
	i32 262, ; 642
	i32 30, ; 643
	i32 179, ; 644
	i32 230, ; 645
	i32 63, ; 646
	i32 201, ; 647
	i32 91, ; 648
	i32 214, ; 649
	i32 88, ; 650
	i32 149, ; 651
	i32 310, ; 652
	i32 203, ; 653
	i32 37, ; 654
	i32 87, ; 655
	i32 251, ; 656
	i32 334, ; 657
	i32 189, ; 658
	i32 51, ; 659
	i32 7, ; 660
	i32 91, ; 661
	i32 334, ; 662
	i32 22, ; 663
	i32 163, ; 664
	i32 97, ; 665
	i32 51, ; 666
	i32 114, ; 667
	i32 267, ; 668
	i32 131, ; 669
	i32 77, ; 670
	i32 28, ; 671
	i32 206, ; 672
	i32 323, ; 673
	i32 244, ; 674
	i32 266, ; 675
	i32 8, ; 676
	i32 200, ; 677
	i32 177, ; 678
	i32 111, ; 679
	i32 267, ; 680
	i32 253 ; 681
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ af27162bee43b7fecdca59b4f67aa8c175cbc875"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
