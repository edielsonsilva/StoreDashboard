﻿using Microsoft.AspNetCore.Components.Forms;

namespace StoreDashboard.Blazor.Server.UI.Components.Shared;

// See https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0#server-validation-with-a-validator-component
public class CustomModelValidation : ComponentBase
{
    private ValidationMessageStore? _messageStore;

    [CascadingParameter] private EditContext? CurrentEditContext { get; set; }

    protected override void OnInitialized()
    {
            if (CurrentEditContext is null)
                throw new InvalidOperationException(
                    $"{nameof(CustomModelValidation)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. " +
                    $"For example, you can use {nameof(CustomModelValidation)} " +
                    $"inside an {nameof(EditForm)}.");

            _messageStore = new ValidationMessageStore(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (s, e) =>
                _messageStore?.Clear();
            CurrentEditContext.OnFieldChanged += (s, e) =>
                _messageStore?.Clear(e.FieldIdentifier);
        }

    public void DisplayErrors(IDictionary<string, ICollection<string>> errors)
    {
            if (CurrentEditContext is not null && errors is not null)
            {
                foreach (var err in errors) _messageStore?.Add(CurrentEditContext.Field(err.Key), err.Value);

                CurrentEditContext.NotifyValidationStateChanged();
            }
        }

    public void ClearErrors()
    {
            _messageStore?.Clear();
            CurrentEditContext?.NotifyValidationStateChanged();
        }
}