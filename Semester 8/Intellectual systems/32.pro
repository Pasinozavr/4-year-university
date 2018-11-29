domains
slist=string*
 
predicates
nondeterm m(string,string).
nondeterm move(string,string).
nondeterm search_dpth(string,string).
nondeterm prolong(slist,slist).
nondeterm dpth(slist,string,slist).
nondeterm show_answer(slist).
nondeterm member(string,slist).
 
clauses
m(a,b).
m(b,c).
m(a,d).
m(b,d).
m(c,d).
m(c,e).
m(d,e).
 
move(A,B):-m(A,B);m(B,A).
 
search_dpth(Start,Finish):-dpth([Start],Finish,Way),show_answer(Way).
 
member(H,[H|_]).
member(H,[_|Tail]):-member(H,Tail).
 
prolong([Temp|Tail],[New,Temp|Tail]):-
    move(Temp,New),not(member(New,[Temp|Tail])).
 
dpth([Finish|Tail],Finish,[Finish|Tail]).
dpth(TempWay,Finish,Way):-
    prolong(TempWay,NewWay),dpth(NewWay,Finish,Way).
 
show_answer([_]):-!.
show_answer([A,B|Tail]):-
    show_answer([B|Tail]),nl,write(B," -> ",A).
goal
search_dpth(a,c),nl,nl,nl,fail.