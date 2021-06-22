/* 
    AEB Pro Document Assembly Methods

    Copyright (C) 2012 -- 2021 AcroTeX.Net
    D. P. Story
    http://www.acrotex.net
    
    Version 1.6.1

    v1.6.1 aebCreateTemplate now returns an object
    v1.6 Added aebAddWatermarkFromText, aebLaunchURL
    v1.5 Added aebCertifyInvisibleSign 
*/

if ( typeof aebTrustedFunctions == "undefined") {
    aebTrustedFunctions = app.trustedFunction( function ( doc, oFunction, oArgs )
    {
        app.beginPriv();
            var retn = oFunction( oArgs, doc );
        app.endPriv();
        return retn;
    });
}
aebAddWatermarkFromFile = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebAddWatermarkFromFile.msg==undefined)?("Add Watermark from File Error: " + e.toString()):aebAddWatermarkFromFile.msg;}
    app.beginPriv();
        try { doc.addWatermarkFromFile(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebAddWatermarkFromFile.msg=undefined;
});
aebAddWatermarkFromText = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.addWatermarkFromText(oArgs);
    app.endPriv();
});
aebImportIcon = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.importIcon(oArgs);
    app.endPriv();
});
aebInsertPages = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebInsertPages.msg==undefined)?("Insert Pages Error: " + e.toString()):aebInsertPages.msg;}
    app.beginPriv();
        try { doc.insertPages(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebInsertPages.msg=undefined;
})
aebAppOpenDoc = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebAppOpenDoc.msg==undefined)?("App Open Doc Error: " + e.toString()):aebAppOpenDoc.msg;}
    app.beginPriv();
        try { return app.openDoc(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebAppOpenDoc.msg=undefined;
    return retn;
})
aebImportTextData = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.importTextData(oArgs);
    app.endPriv();
});
aebImportSound = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.importSound(oArgs);
    app.endPriv();
});
aebSaveAs = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        app.execMenuItem("Save");
    app.endPriv();
});
aebDocSaveAs = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebDocSaveAs.msg==undefined)?("Doc SaveAs Error: " + e.toString()):aebDocSaveAs.msg;}
    app.beginPriv();
    try { 
        return doc.saveAs(oArgs); 
    } catch(e){console.println(Msg(e));}
    app.endPriv();
    aebDocSaveAs.msg=undefined;

});
aebExtractPages = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.extractPages(oArgs);
    app.endPriv();
});
aebMailDoc = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.mailDoc(oArgs);
    app.endPriv();
});
aebImportDataObject = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.importDataObject(oArgs);
    app.endPriv();
});
aebSignatureSign = app.trustPropagatorFunction( function ( oArgs, field )
{
    app.beginPriv();
        return field.signatureSign(oArgs);
    app.endPriv();
});
aebSecurityHandlerLogin = app.trustPropagatorFunction( function ( oArgs, securityHandler )
{
    app.beginPriv();
        return securityHandler.login(oArgs);
    app.endPriv();
});
aebTimestampSign = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return doc.timestampSign(oArgs);
    app.endPriv();
});
aebSecurityGetHandler = app.trustPropagatorFunction( function ( oArgs, security )
{
    app.beginPriv();
        return security.getHandler(oArgs);
    app.endPriv();
});
aebAppGetPath = app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        return app.getPath(oArgs);
    app.endPriv();
    return retn;
});
aebSignatureSetSeedValue = app.trustPropagatorFunction( function ( oArgs, field )
{
    app.beginPriv();
        return field.signatureSetSeedValue( oArgs )
    app.endPriv();
});
aebCertifyInvisibleSign = app.trustPropagatorFunction( function ( oArgs, field )
{
    app.beginPriv();
        return field.certifyInvisibleSign( oArgs )
    app.endPriv();
});
aebAddIcon=app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
        doc.addIcon(oArgs);
    app.endPriv();
});
aebCreateTemplate = app.trustPropagatorFunction( function ( oArgs, doc )
{
    var Msg=function(e){return (aebCreateTemplate.msg==undefined)?("Create Template Error: " + e.toString()):aebCreateTemplate.msg;}
    app.beginPriv();
        try { return doc.createTemplate(oArgs); } catch(e) {console.println(Msg(e));}
    app.endPriv();
    aebCreateTemplate.msg=undefined;

})
aebBrowseForDoc=app.trustPropagatorFunction( function ( oArgs, doc )
{
    app.beginPriv();
      var retn = app.browseForDoc(oArgs);
    app.endPriv();
});
aebLaunchURL=app.trustPropagatorFunction ( function ( oArgs )
{
    app.beginPriv();
      var retn = app.launchURL(oArgs);
    app.endPriv();
});
