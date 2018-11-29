domains
dom_parent=parent(symbol,symbol)
database
parent(symbol,symbol)
predicates
nondeterm man(symbol)
nondeterm woman(symbol)
nondeterm father(symbol,symbol)
nondeterm mother(symbol,symbol)
nondeterm son(symbol,symbol)
nondeterm daught(symbol,symbol)
nondeterm sisbros(symbol,symbol)
nondeterm dvourbrat(symbol,symbol)
nondeterm who_is_the_dvourbrat

nondeterm command(integer)
nondeterm read_command
nondeterm main

nondeterm input_parent
nondeterm output_parent

clauses

man(sergey).
man(oleg).
man(pasha).
man(vanya).
man(iliya).
woman(dasha).
woman(masha).
woman(nastya).
woman(marina).
woman(ksusha).

father(Father,Child):-parent(Father,Child),man(Father).
mother(Mother,Child):-parent(Mother,Child),woman(Mother).
son(Boy,Parent):-parent(Parent,Boy),man(Boy).
daught(Girl,Parent):-parent(Parent,Girl),woman(Girl).
sisbros(Who,Whom):-parent(_,Who),parent(_,Whom),Who<>Whom.
dvourbrat(Kto,Komu):-son(Kto,A),parent(B,Komu),sisbros(A,B),Kto<>Komu.
who_is_the_dvourbrat :- dvourbrat(Kto,Komu), write(Kto," ��� ���������� ���� ��� ",Komu, "."),nl.
input_parent:-readchar(X),X='.', !.
input_parent:-readterm(dom_parent, parent(X,Y)), assert(parent(X,Y)).
output_parent:-write("������ ������:",'\n'),parent(X,Y), write(X," is parent for ",Y), nl, fail.
command(1):-write("������� �������� ������������ �����, �������� parent(\"pasha\", \"masha\"). ����������� ��� ������ �����, ��� ��� ���������, � ������ sergey, oleg, pasha, vanya, iliya, dasha, masha, nastya, marina, ksusha",'\n'), 
		nl, input_parent,fail.
command(2):-parent(X,Y), save("C:\\Temp\\base.txt"),write("��������"),nl,fail.
command(3):-consult("C:\\Temp\\base.txt"),write("������� ���������������� ��"),nl,fail.
command(4):-write("������ ���������� �������:",'\n'),who_is_the_dvourbrat,nl,fail.
command(6):-output_parent,nl,fail.
command(7):-consult("C:\\Temp\\ready.txt"),write("������� ������� ��"),nl,fail.
command(1):-main.
command(2):-main.
command(3):-main.
command(4):-main.
command(6):-main.
command(7):-main.
command(5).
read_command:-write(">"),readint(C),command(C).
main:-
write("���� ������"),nl,
write("1 - ��������� ��"),nl,
write("2 - ��������� ��"),nl,
write("3 - ������� ��"),nl,
write("4 - ����� ���������� �������"),nl,
write("5 - �����"),nl,
write("6 - ������� ��"),nl,
write("7 - ������� ����������� ������� ��"),nl,
read_command.

goal
main.
