Imports System.IO
Imports System.Web
Imports System.Web.Mvc

Namespace Microsoft.Web.Mvc
	' in Global.asax.vb Application_Start you can insert these into the ViewEngine chain like so:
	'
	' ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine())
	'
	' or
	'
	' ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine("iPhone")
	' {
	'     ContextCondition = (ctx => ctx.Request.UserAgent.IndexOf(
	'         "iPhone", StringComparison.OrdinalIgnoreCase) >= 0)
	' });

	Public Class MobileCapableWebFormViewEngine
		Inherits WebFormViewEngine
		
		Private m_ViewModifier As String
		Private m_ContextCondition As Func(Of HttpContextBase, Boolean)
		
		Public Property ViewModifier() As String
			Get
				Return m_ViewModifier
			End Get
			Set
				m_ViewModifier = Value
			End Set
		End Property
		
		Public Property ContextCondition() As Func(Of HttpContextBase, Boolean)
			Get
				Return m_ContextCondition
			End Get
			Set
				m_ContextCondition = Value
			End Set
		End Property

		Public Sub New()
			Me.New("Mobile", Function(context) context.Request.Browser.IsMobileDevice)
		End Sub

		Public Sub New(viewModifier As String)
			Me.New(viewModifier, Function(context) context.Request.Browser.IsMobileDevice)
		End Sub

		Public Sub New(viewModifier As String, contextCondition As Func(Of HttpContextBase, Boolean))
			Me.ViewModifier = viewModifier
			Me.ContextCondition = contextCondition
		End Sub

		Public Overrides Function FindView(controllerContext As ControllerContext, viewName As String, masterName As String, useCache As Boolean) As ViewEngineResult
			Return NewFindView(controllerContext, viewName, Nothing, useCache, False)
		End Function

		Public Overrides Function FindPartialView(controllerContext As ControllerContext, partialViewName As String, useCache As Boolean) As ViewEngineResult
			Return NewFindView(controllerContext, partialViewName, Nothing, useCache, True)
		End Function

		Private Function NewFindView(controllerContext As ControllerContext, viewName As String, masterName As String, useCache As Boolean, isPartialView As Boolean) As ViewEngineResult
			If Not ContextCondition(controllerContext.HttpContext) Then
					' we found nothing and we pretend we looked nowhere
				Return New ViewEngineResult(New String() {})
			End If

			' Get the name of the controller from the path
			Dim controller As String = controllerContext.RouteData.Values("controller").ToString()
			Dim area As String = ""
			Try
				area = controllerContext.RouteData.DataTokens("area").ToString()
			Catch
			End Try

			' Apply the view modifier
			Dim newViewName = String.Format("{0}.{1}", viewName, ViewModifier)

			' Create the key for caching purposes          
			Dim keyPath As String = Path.Combine(area, controller, newViewName)

			Dim cacheLocation As String = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, keyPath)

			' Try the cache          
			If useCache Then
				'If using the cache, check to see if the location is cached.                              
				If Not String.IsNullOrWhiteSpace(cacheLocation) Then
					If isPartialView Then
						Return New ViewEngineResult(CreatePartialView(controllerContext, cacheLocation), Me)
					Else
						Return New ViewEngineResult(CreateView(controllerContext, cacheLocation, masterName), Me)
					End If
				End If
			End If
			Dim locationFormats As String() = If(String.IsNullOrEmpty(area), ViewLocationFormats, AreaViewLocationFormats)

			' for each of the paths defined, format the string and see if that path exists. When found, cache it.          
			For Each rootPath As String In locationFormats
				Dim currentPath As String = If(String.IsNullOrEmpty(area), String.Format(rootPath, newViewName, controller), String.Format(rootPath, newViewName, controller, area))

				If FileExists(controllerContext, currentPath) Then
					ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, keyPath, currentPath)

					If isPartialView Then
						Return New ViewEngineResult(CreatePartialView(controllerContext, currentPath), Me)
					Else
						Return New ViewEngineResult(CreateView(controllerContext, currentPath, masterName), Me)
					End If
				End If
			Next
			Return New ViewEngineResult(New String() {})
			' we found nothing and we pretend we looked nowhere
		End Function
	End Class
End Namespace
