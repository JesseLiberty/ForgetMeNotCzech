using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgetMeNot.ViewModel;

[ObservableObject]
public partial class MainViewModel
{
  [ObservableProperty] private bool flowerIsVisible = true;


  [RelayCommand]
  private void ImageTapped()
  {
    FlowerIsVisible = !FlowerIsVisible;
  }
}

