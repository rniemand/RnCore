namespace RnCore.MediaInfoAbstractions;

public interface ILanguageMediaStreamAbstraction : IMediaStreamAbstraction
{
	bool Default { get; }
	bool Forced { get; }
	string Language { get; }
	int Lcid { get; }
	long StreamSize { get; }
}
