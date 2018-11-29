domains
parent=symbol

predicates
nondeterm parent(symbol,symbol)
nondeterm man(symbol)
nondeterm woman(symbol)
nondeterm father(symbol,symbol)
nondeterm mother(symbol,symbol)
nondeterm son(symbol,symbol)
nondeterm daught(symbol,symbol)
nondeterm sisbros(symbol,symbol)
nondeterm dvourbrat(symbol,symbol)
nondeterm who_is_the_dvourbrat

clauses
parent(dasha,marina).
parent(dasha,iliya).
parent(oleg,marina).
parent(oleg,iliya).
parent(marina, ksusha).
parent(iliya, sergey).
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
who_is_the_dvourbrat :- dvourbrat(Kto,Komu), write(Kto," is the dvourbrat of ",Komu, "."),nl.
goal
who_is_the_dvourbrat.
