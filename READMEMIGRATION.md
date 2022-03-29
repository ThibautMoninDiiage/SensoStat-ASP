# Introduction

Le principe de ce README est d'expliquer � quoi va servire le fait de faire des migrations et comment en faire en �tant en code first

# But d'une migration

Le but d'une migration est de pouvoir effectuer des modifications sur la base de donn�es sans pour autant avoir besoin de la supprimer,
ce qui va nous permettre de pouvoir ajouter des champs ou carr�ment des nouvelles tables

#Comment r�aliser une migration

## Etape 1 : Activer la migration
Une fois votre code ex�cut� une premi�re fois et votre base de donn�e cr�e grace � votre code, vous pouvez ouvrir la console de gestionnaire de package

Pour l'ouvrir aller dans Outils -> Ligne de commande -> Invite de commande de package Nuget

Puis une fois votre console ouverte, vous tapez la ligne de commande : Enable-Migrations

Cela va ajouter un dossier migrations qui va contenir deux fichiers : 

		- Une classe configuration qui sera g�n�r� toute seul du fait que vous soyez en code first
		- Une migration InitialCreate qui contiendra d�ja toutes les infos relatives � la base de donn�es que nous avons g�n�r� juste avant


## Etape 2 : G�rer ses migrations

Il y a deux commandes que nous pouvons taper par la suite:

		- La premi�re est Add-Migration. Cette commande va nous permettre de pouvoir faire des ajouts de champs et de table dans la base de donn�es
		  Elle nous permet de nommer la migration ce qui nous permettra d'avoir une trace des migrations que nous allons faire. Pour cela il suffira de taper la commande Add-Migration "Nom de la migration"
		  Dedans, on va donc pouvoir d�crire dans m�thode Up() toutes les cr�ation et modifications de champs et tables existantes que nous allons vouloir faire
		  Dans la m�thode Down(), on va pouvoir sp�cifier tous ce qui est suppression et autre.


		- La deuxi�me commande est Update-Database. Elle va nous permettre d'effectuer tout ce qui va �tre �crit dans les fichier de migration. On peut doc avoir un comme plusieurs fichier � ex�cuter.