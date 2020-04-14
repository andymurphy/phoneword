using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Phoneword
{
    
    public partial class OldPage : ContentPage
    {
        // Store the Entry and the two Button controls in class fields, so we can interact with them later
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;

        // Constructor
        public OldPage()
        {
            // This is the space between the edge of the main page and the stackpanel we will create
            this.Padding = new Thickness(20);

            // Create the stackpanel which will hold our controls
            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };
            // Add the label, text entry and the two buttons to the stackpanel
            panel.Children.Add(new Label{
                Text = "Enter a phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry            {
                Text = "1-800-Xamarin",

            });
            panel.Children.Add(translateButton = new Button
            {
                Text = "Translate"                
            });
            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false
            });            
            // Add a click handler for the translate and call buttons
            translateButton.Clicked += OnTranslate;
            callButton.Clicked += OnCall;         
            // Assign the stackpanel to the content property of the MainPage (this)
            this.Content = panel;
        }
        /// <summary>
        /// Handles the user pressing the call button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void OnCall(object sender, EventArgs e)
        {
            // TODO display an alert asking the user if they want to call the translated number.
            // Have OK and Cancel buttons
            if (await this.DisplayAlert("Dial a number?", "Do you want to call " + translatedNumber + "?", "Yes", "No"))
            {
                //  Dial the phone number
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");                    
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
                }
                catch (Exception)
                {
                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }

        /// <summary>
        /// Handles the user clicking the Translate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTranslate(object sender, EventArgs e)
        {
            // Get the phonenumber from the entry field
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                // We have a translated number
                // Enable the call button
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                // We got nothing back for the translated number so disable the button
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }
    }
}
