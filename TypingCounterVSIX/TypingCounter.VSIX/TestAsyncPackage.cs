using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using Microsoft.VisualStudio.Threading;
using IAsyncServiceProvider = Microsoft.VisualStudio.Shell.IAsyncServiceProvider;
using Microsoft.VisualStudio.Text.Editor;

namespace TypingCounter.VSIX
{
	[ProvideService((typeof(STextWriterService)), IsAsyncQueryable = true)]
	[ProvideService((typeof(IKeyProcessorProvider)), IsAsyncQueryable = true)]
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(TestAsyncPackage.PackageGuidString)]
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
	public sealed class TestAsyncPackage : AsyncPackage
	{
		public const string PackageGuidString = "18045615-79d8-4ee5-848e-05a7d20d9a0a";
		public TestAsyncPackage()
		{
		}

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
		/// <param name="progress">A provider for progress updates.</param>
		/// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			// When initialized asynchronously, the current thread may be a background thread at this point.
			// Do any initialization that requires the UI thread after switching to the UI thread.
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
			AddService(typeof(STextWriterService), CreateTextWriterService);
			AddService(typeof(IKeyProcessorProvider), CreateKeyProcessorProvider);

			ITextWriterService textService = await this.GetServiceAsync(typeof(STextWriterService)) as ITextWriterService;
			string userpath = @"C:\Informal\Temp\MyFile.txt";
			await textService.WriteLineAsync(userpath, "this is a test");
        }

		public Task<object> CreateKeyProcessorProvider(IAsyncServiceContainer container, CancellationToken cancellationToken, Type serviceType)
		{
			return Task.FromResult(new ButtonProvider() as object);
		}

		public async Task<object> CreateTextWriterService(IAsyncServiceContainer container, CancellationToken cancellationToken, Type serviceType)
		{
			TextWriterService service = new TextWriterService(this);
			await service.InitializeAsync(cancellationToken);
			return service;
		}
	}
}
