using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Editor;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace TypingCounter.VSIX
{
	[Export(typeof(IKeyProcessorProvider))]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    [ContentType("any")]
    [Name("ButtonProvider")]
    [Order(Before = "default")]
    internal class ButtonProvider : IKeyProcessorProvider
    {
        [ImportingConstructor]
        public ButtonProvider()
        {
        }

        public KeyProcessor GetAssociatedProcessor(IWpfTextView wpfTextView)
        {
            return new ButtonKeyProc(wpfTextView);
        }
    }


    internal class ButtonKeyProc : KeyProcessor
    {
        internal static event KeyEventHandler KeyDownEvent;

        public ButtonKeyProc(ITextView textView)
        {
        }

		public override void KeyDown(KeyEventArgs args)
        {
               var x = 2;        
        }
    }
}
