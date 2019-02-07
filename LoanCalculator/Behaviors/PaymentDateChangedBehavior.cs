using Syncfusion.SfPicker.XForms;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace LoanCalculator
{
    public class PaymentDateChangedBehavior : Behavior<SfPicker>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(PaymentDateChangedBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public SfPicker AssociatedObject { get; private set; }

        protected override void OnAttachedTo(SfPicker bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.OkButtonClicked += OnPickerDateChanged;
        }

        protected override void OnDetachingFrom(SfPicker bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.OkButtonClicked -= OnPickerDateChanged;
            AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        private void OnPickerDateChanged(object sender, EventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            if (Command.CanExecute(null))
            {
                Command.Execute(null);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}