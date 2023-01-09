global using System.Text.Json;
global using WritingCodeWithCode.Specification;
global using Microsoft.CodeAnalysis.CSharp.Syntax;
global using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CSharp;

var path = Directory.GetCurrentDirectory();

Schema? schema = null;

await using (var fileStream = File.OpenRead(Path.Combine(path, "spec.json")))
{
    schema = await JsonSerializer.DeserializeAsync<Schema>(fileStream, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });
}

if (schema is null)
    throw new Exception("Unable to deserialise schema");

var members = schema.Types.Select(t => CreateClass(t.TypeName, t.Properties)).ToArray()
    ?? Array.Empty<MemberDeclarationSyntax>();

var ns = FileScopedNamespaceDeclaration(IdentifierName("CodeGen")).AddMembers(members);

await using var streamWriter = new StreamWriter(@"c:\code-gen\generated.cs", false);

ns.NormalizeWhitespace()
    .WriteTo(streamWriter);

static ClassDeclarationSyntax CreateClass(string name, IEnumerable<SchemaProperty> properties)
{
	var classDeclaration = ClassDeclaration(Identifier(name))
		.AddModifiers(Token(SyntaxKind.PublicKeyword));

    foreach (var property in properties)
    {
        classDeclaration = classDeclaration.AddMembers(
            PropertyDeclaration(
                IdentifierName(property.Type),
                Identifier(property.Name))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))));
    }

    return classDeclaration;
}
