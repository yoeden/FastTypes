# Workflow

## Content

**[1. Branches](#heading--branches)**
  * [1.1. Master](#heading--branches-master)
  * [1.2. Feature/Bug](#heading--branches-feature)
  * [1.3. Release](#heading--branches-release)
  * [1.4. Hotfix](#heading--branches-hotfix)

**[2. BBCode formatting](#heading--2)**
  * [2.1. Basic text formatting](#heading--2-1)
      * [2.1.1. Not so basic text formatting](#heading--2-1-1)
  * [2.2. Lists, Images, Code](#heading--2-2)
  * [2.3. Special features](#heading--2-3)

<div id="heading--branches"/>

## Branches 🌴

<div id="heading--branches-master"/>

### master 👽
The `master` branch is for production only and contains the latest approved code changes that are ready for the next release.

_No direct commits are allowed on the `master` branch._
_anything in the _master_ branch is deployable._

<div id="heading--branches-feature"/>

### feature 👷

`Feature` branches are a short-lived developement branches, either for bug fixes or feature development.
They will branch from `master` and merged back to `master`.

<div id="heading--branches-release"/>

### release 🖋️

`Release` branches are a long-lived tagged branches, created from main at a certain point in development.

<div id="heading--branches-hotfix"/>

### hotfix ☕

`Hotfixes` branches are a short-lived developement branches, only for critical bugs and hotfixes.
They will branch from the relevant `release` branch, and merged back to its parent.


## Flows 🌊

### Developing a feature / Fixing a bug 👷

1. Branch out of `master`.
2. Name the branch : 
   1. In case of a feture, use `feature/_DESC` (_user1/feature/adding-support-for-something_).
   2. In case of a bug, use `bug/_ID_/_DESC` (_user1/bug/1337/engine-wont-start-fix_).
3. Develop the changes and commit them locally.
4. Create a pull request into `master`.
5. If changes are fitted for the upcoming version and passes code review and CI build, it will be merged for `master`.

### Creating a release 🖋️

1. Define `done` for the current release.
2. Branch out of `master` and name the branch `release/vX.X.X`.
3. Create a tag named `vX.X.X`.

### Hot fixing ☕

1. When evaluation is done, Branch out of `release/vX.X.X` and name the branch `hotfix/VERSION/ID/DESC` (_hotfix/v0.1.2b/1337/fixed-hotcoffeemod-notinstalled_)
2. Develop the changes and commit them locally.
3. TBD

