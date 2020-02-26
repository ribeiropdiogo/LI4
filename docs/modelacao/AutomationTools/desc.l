%{
#include <stdlib.h>

void resetbuffers();
void openUC(char *title);
void closeUC();

void beginEnum(char * extra);
void endEnum();

void showDesc(char *desc);
void showCenario(char *cena);
void showPrec(char * prec);
void showPosc(char * posc);
void showFn();
void showItem(char * text, int len);
void showFa(char * text, int len);
void showFe(char * text, int len);

void boldItem(char *tag, char *text);
void boldsubItem(char *tag, char *text);

char *getTitle(char * unproc, int len);
char *getDesc(char * unproc, int len);
char *getCenario(char * unproc, int len);
char *getPrec(char * unproc, int len);
char *getPosc(char * unproc, int len);

int altFluxos, excFluxos, curr, sub;
%}

Abrev ER
p [^\n\r]+
s [ \t\r]+
%%
\<UC\>\n                        {resetbuffers();}
TIT:[^\n\r]+\n	                {openUC(getTitle(yytext, yyleng));}
DES:[^\n\r]+\n                  {showDesc(getDesc(yytext, yyleng));}
CEN:[^\n\r]+\n                  {showCenario(getCenario(yytext, yyleng));}
PreC:[^\n\r]+\n                 {showPrec(getPrec(yytext, yyleng));}
PosC:[^\n\r]+\n                 {showPosc(getPosc(yytext, yyleng));}
FN:\n                           {showFn();}
FA\ [0-9]+:[^\n]+\n             {showFa(yytext, yyleng);}
FE\ [0-9]+:[^\n]+\n             {showFe(yytext, yyleng);}
[ \t]+\*\ \([^\)]+\)\ [^\n\r]+  {showItem(yytext, yyleng);}
\<\/UC\>						{closeUC();}
%%

void resetbuffers()
{
	altFluxos = excFluxos = 0;
	sub = -1;
}

void openUC(char *title)
{
	printf("\\subsection{%s}\n", title);
	printf("\\begin{itemize}\n");
	boldItem("Use Case:", title);
}

void closeUC()
{
	endEnum();
	printf("\\end{itemize}\n");
}

void beginEnum(char * extra)
{
	printf("\\begin{enumerate}%s\n", extra);
}

void endEnum()
{
	printf("\\end{enumerate}\n");
}

void showDesc(char *desc)
{
	boldsubItem("Descrição:", desc);
}

void showCenario(char *cena)
{
	boldsubItem("Cenários:", cena);
}

void showPrec(char * prec)
{
	boldsubItem("Pré-Condição:", prec);
}

void showPosc(char * posc)
{
	boldsubItem("Pós-Condição:", posc);
}

void showFn()
{
	curr = 1;
	boldsubItem("Fluxo Normal:","");
	beginEnum("");
}

void showItem(char * text, int len)
{
	text[len] = '\0';
	text = memchr(text, 41, len) + 2;
	printf("\\item %s", text);
}

void showFa(char * text, int len)
{
	endEnum();
	char buff[1024];
	char buff2[100];
	int passo = 0;
	int passo2 = 0;
	text[len-1] = '\0';
	sscanf(text, "FA %d:[%[^]\t\n]](passo %[^)])\n",&passo,buff,buff2);
	printf("\\subitem \\textbf{Fluxo Alternativo %d:[%s](passo %s)}\n", ++excFluxos, buff, buff2);
	printf("\\begin{enumerate}[{%s.}1]\n", buff2);
}

void showFe(char * text, int len)
{
	endEnum();
	char buff[1024];
	char buff2[100];
	int passo = 0;
	int passo2 = 0;
	text[len-1] = '\0';
	sscanf(text, "FE %d:[%[^]\t\n]](passo %[^)])\n",&passo,buff,buff2);
	printf("\\subitem \\textbf{Fluxo de Exceção %d:[%s](passo %s)}\n", ++excFluxos, buff, buff2);
	printf("\\begin{enumerate}[{%s.}1]\n", buff2);
}

void boldItem(char *tag, char *text)
{
	printf("\\item \\textbf{%s} %s\n", tag, text);
}

void boldsubItem(char *tag, char *text)
{
	printf("\\subitem \\textbf{%s} %s\n", tag, text);
}

char *getTitle(char * unproc, int len)
{
	unproc[len - 1] = '\0';
	return unproc + 4;
}

char *getDesc(char * unproc, int len)
{
	return getTitle(unproc, len);
}

char *getCenario(char * unproc, int len)
{
	return getTitle(unproc, len);
}

char *getPrec(char * unproc, int len)
{
	return getTitle(unproc, len) + 1;
}

char *getPosc(char * unproc, int len)
{
	return getPrec(unproc, len);
}

int yywrap()
{ return(1); }

int main()
{ 
resetbuffers();
yylex();
return 0;
}
