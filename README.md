# XAKProgressHUD
An implement of KProgressHUD for Xamarin Android, similar to KProgressHUD for Java Android.
## How to download
Install this plugin into your Xamarin.Android projects. You can download it from Nuget
```csharp
Install-Package KProgressHUD -Version 1.0.0
```
## How to use
Declare the package like below and you can customize KProgressHUD like below
```csharp
using KK = KProgressHUD.KProgressHUD;
```
### For Indeterminate
Progress Loading starts immediately
```csharp
KK hud = KK.Create(this)
           .SetLabel("Please wait...")
           .SetDetailsLabel("File Downloading...")
           .SetDimAmount(0.5f)
           .SetWindowColor(Resources.GetColor(Resource.Color.my_gray))
           .SetAnimationSpeed(2);
hud.Show();
// To dismiss
// Use "hud.Dismiss()"
```
### For Determinate
Progress Loading starts after specified time
```csharp
KK hud = KK.Create(this)
           .SetLabel("Please wait...")
           .SetDetailsLabel("File Downloading...")
            .SetGraceTime(1000)
           .SetDimAmount(0.5f)
           .SetWindowColor(Resources.GetColor(Resource.Color.my_gray))
           .SetAnimationSpeed(2);
hud.Show();
```
### For Apply Custom Views
You can use custom views instead of default loader
```csharp
ImageView imageView = new ImageView(this);
imageView.SetBackgroundResource(Resource.Drawable.spin_animation);
AnimationDrawable drawable = (AnimationDrawable)imageView.Background;
drawable.Start();
KK hud = KK.Create(this)
           .SetLabel("Loading...")
           .SetCustomView(imageView);
hud.Show();
```
## Demo
<table>
  <tr>
    <th>Indeterminate</th>
    <th>With Label</th>
    <th>With Label and Detail</th>
  </tr>
  <tr>
    <td><img src="https://raw.githubusercontent.com/androidmads/XAKProgressHUD/master/Sample/indeterminate.png" alt="Indeterminate" style="width:200px;height:228px;"></td>
    <td><img src="https://raw.githubusercontent.com/androidmads/XAKProgressHUD/master/Sample/with_label.png" alt="With Label" style="width:200px;height:228px;"></td>
    <td><img src="https://raw.githubusercontent.com/androidmads/XAKProgressHUD/master/Sample/with_label_details.png" alt="With Detail" style="width:200px;height:228px;"></td>
  </tr>  
  <tr>
    <th>Custom View</th>
    <th>Dim Background</th>
    <th>Custom Color</th>
  </tr>
  <tr>
    <td><img src="https://raw.githubusercontent.com/androidmads/XAKProgressHUD/master/Sample/custom_view.png" alt="Custom View" style="width:200px;height:228px;"></td>
    <td><img src="https://raw.githubusercontent.com/androidmads/XAKProgressHUD/master/Sample/dim_background.png" alt="Dim Background" style="width:200px;height:228px;"></td>
    <td><img src="https://raw.githubusercontent.com/androidmads/XAKProgressHUD/master/Sample/custom_color.png" alt="With Detail" style="width:200px;height:228px;"></td>
  </tr>  
</table>

For more details [click here](https://github.com/androidmads/XAKProgressHUD/blob/master/Sample/MainActivity.cs)

## License
```
MIT License

Copyright (c) 2017 AndroidMad / Mushtaq M A

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
