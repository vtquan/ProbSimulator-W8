﻿

#pragma checksum "C:\Users\vtquan\Documents\GitHub\ProbSimulator-W8\Probability Simulator\Pages\CoinPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "410E8017294C2BDF577ACE917BDAE73D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Probability_Simulator.Pages
{
    partial class CoinPage : global::Probability_Simulator.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 57 "..\..\Pages\CoinPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.coinFlipB_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 75 "..\..\Pages\CoinPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).SizeChanged += this.graphBox_SizeChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 59 "..\..\Pages\CoinPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.CoinImage_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 36 "..\..\Pages\CoinPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


