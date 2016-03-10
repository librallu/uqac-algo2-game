

public abstract class ObjectiveTree
{

    /*
        Est appelé Pour simplifier l'arbre.
        Retourne l'arbre simplifié
    */
    public abstract ObjectiveTree simplify();

    public abstract string toString();

}



/**** DEFINITION DES FEUILLES DE L ARBRE *****/


/*
    Pour ajouter un objectif à l'arbre (par exemple tuer 5 ennemis),
    définir une nouvelle classe qui aura une méthode 
    simplify() qui retournera soit elle même si non complétée, sinon
    Si réussie, retourner un nouvel objet ObjectiveOk et si
    échouée, retourner un nouvel objet ObjectiveNok
*/
public abstract class ObjectiveLeaf : ObjectiveTree
{
    /*
        Dans le cas où il s'agit d'une feuille, on retourne
        l'élément lui-même car il ne peut pas être simplifié
    */
    public override ObjectiveTree simplify()
    {
        return this;
    }
}

public class ObjectiveOk : ObjectiveLeaf
{
    public ObjectiveOk()
    {

    }

    public override string toString()
    {
        return "Objectives Done !";
    }
}
public class ObjectiveNok : ObjectiveLeaf
{
    public ObjectiveNok()
    {

    }

    public override string toString()
    {
        return "false";
    }
}




/******* DEFINITION DES OPERATEURS SUR L ARBRE ******/

public abstract class ObjectiveBinary : ObjectiveTree
{
    public ObjectiveTree left, right;

    public ObjectiveBinary(ObjectiveTree l, ObjectiveTree r)
    {
        left = l;
        right = r;
    }

    /*
        Simplification de l'arbre : 
        On simplifie les sous-arbres, ensuite, 
        on regarde si les deux sont des feuilles,
        si oui, on peut simplifier le noeud courant.
    */
    public override ObjectiveTree simplify()
    {
        left = left.simplify();
        right = right.simplify();
        if (left is ObjectiveLeaf || right is ObjectiveLeaf)
        {
            return operatorSimplify();
        }
        else
        {
            return this;
        }
    }

    public abstract ObjectiveTree operatorSimplify();
}

public class ObjectiveAnd : ObjectiveBinary
{

    public ObjectiveAnd(ObjectiveTree l, ObjectiveTree r) : base(l, r)
    {

    }

    public override ObjectiveTree operatorSimplify()
    {
        if (left is ObjectiveOk && right is ObjectiveOk)
        {
            return new ObjectiveOk();
        }
        else if (left is ObjectiveNok || right is ObjectiveNok)
        {
            return new ObjectiveNok();
        } else { return this; }
    }

    public override string toString() { return "AND"; }
}

public class ObjectiveOr : ObjectiveBinary
{

    public ObjectiveOr(ObjectiveTree l, ObjectiveTree r) : base(l, r)
    {

    }

    public override ObjectiveTree operatorSimplify()
    {
        if ( left is ObjectiveOk || right is ObjectiveOk ) { return new ObjectiveOk(); }
        if (left is ObjectiveNok && right is ObjectiveNok) { return new ObjectiveNok(); }
        return this;

    }

    public override string toString() { return "OR"; }
}