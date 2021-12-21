## MyFlow: A successful git branching model for DevOps teams

The most popular workflow for git is still [git-flow](https://nvie.com/posts/a-successful-git-branching-model). I did a poll on Twitch with a friend some time ago and still over 50% of the audience said they were using it. And this is strange, because even [Vincent Driessen](https://nvie.com/about/), the author of the original post about git-flow, wrote a note in March 2020 in the post that git-flow is not the best workflow for cloud services, apps, or web applications. So why is it still so popular? I think there are three reasons:

1. It’s very detailed, specific, and it provides guidance for people that are new to git.
2. It has a great name that is good to memorize.
3. It has a good visualization

And what’s the problem with GitHub Flow? Well, the name sounds too close to GitFlow and the description of the workflow is not very precise. How do you deploy? How do you work with long-lived feature branches? How do you support different versions for different customers?

I thinks what we need is a good new workflow with a good name and a nice chart! So let me introduce you to…

## MyFlow

MyFlow is a lightweight, trunk-based workflow based on pull-requests. MyFlow is not a new invention! Many teams already work this way. It is a very natural way to branch and merge if you focus on collaboration with pull-request. I just try to give it a name and add some explanations for engineers that are new to git. So lets start…

![B17827_11_005](https://user-images.githubusercontent.com/5276337/146935839-ef38cba5-ec75-463f-8f4c-a7be79ca2e14.png)

### The main branch

MyFlow is [trunk-based](https://trunkbaseddevelopment.com/) – that means there is only one main branch called main. The main branch should always be in a clean state in which a new release can be created at any time. That’s why you should protect your main branch with [a branch protection rule](https://docs.github.com/en/github/administering-a-repository/about-protected-branches). A good branch protection rule would include:

- Require a minimum of two pull request reviews before merging.
- Dismiss stale pull request approvals when new commits are pushed.
- Require reviews from [Code Owners](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/about-code-owners).
- Require status checks to pass before merging that includes your CI build, test execution, code analysis, and linters.
- Include administrators in the restrictions.
- Permit force pushes.

The more you automate using the the workflow that is triggered by your pull request, the more likely it is that you can keep your branch in a clean state.

All other branches are always branched of main. Since this is your default branch, you never have to specify a source branch when you create new branches. This simplifies things and removes a source of error.

### Private topic branches

Private topic branches can be used to work on new features, documentation, bugs, infrastructure, and everything else that is in your repository. They are private – so they belong to one specific user. Other team members can check out the branch to test it – but they are not allowed to directly push changes to this branch. Instead they must use [suggestions](http://haacked.com/archive/2019/06/03/suggested-changes/) in pull requests to suggest changes to the author of the pull request.

To indicate that the branches are private, I recommend a naming convention like users/* or private/* that makes this obvious. Don’t use naming conventions like features/* – they imply that multiple developers may work on one feature. I also recommend including the id of the issue or bug in the name. This makes it easy to reference it later in the commit message. A good convention would be:

```
users/<username>/<id>_<topic>
```

Private topic branches are low-complexity branches. If you work on a more complex feature, you should at least merge your changes back to main and delete the topic branch once a day using feature flags (aka feature toggles). If the changes are simple and can be easily rebased onto main, you can leave the branch open for a longer time. The branches are low-complexity and not short-lived.

So, to start working on a new topic you create a new branch locally:

```console
$ git switch -c <branch-name>
```

For example:

```console
$ git switch -c users/kaufm/42_my-new-feature main
> Switched to new branch 'users/kaufm/42_my-new-feature main'
```

Create your first modifications and commit and push them to the server. It does not matter what you modify – you could just add a blank line to a file. You can overwrite the change later anyway. Add, commit, and push the change:

```console
$ git add .
$ git commit
$ git push --set-upstream origin <branch-name>
```

For example:

```console
$ git add .
$ git commit -m "New feature #42"
$ git push --set-upstream origin users/kaufm/42_new-feature
```
Note that I did not specify the `-m` parameter in git commit to specify the commit message. I prefer that my default editor opens the `COMMIT_EDITMSG` so that I can edit the commit message there. This way I can see the files with changes and I have a visual help where the lines should break. Make sure you have set your editor correct if you want to do this. If, for example, you prefer visual studio code, you can set it as your default editor with:  `$ git config --global core.editor "code --wait"`.

<img width="906" alt="2021-12-03_20-23-04" src="https://user-images.githubusercontent.com/5276337/146937583-be3f33f8-54a2-49cd-9975-94ca597393d9.png">

Now you create a pull request in draft mode. This way the team knows that you are working on that specific topic. A quick view on the list of open pull requests should give you a nice overview on the topics the team is currently working on.

> Note that I use the [GitHub CLI](https://cli.github.com/) to interact with pull requests as I find it easier to read and understand than to use screenshots of the web UI. You can do the same using the web UI.

```console
$ gh pr create --fill --draft
```

The option `--fill` will automatically set the title and description of the pr from your commit. If you have omitted the `-m` argument when committing your changes and if you have added a multi-line commit message in your default editor, the first line will be the title of the pull request and the rest of the message the body. You could also set title (`--title` or `-t`) and body (`--body` or `-b`) directly when creating the pull request instead of using the `--fill` option.

You can now start working on your topic. And you can use the full power of git. If you want to add changes to your previous commit, for example, you can do so with the `--amend` option:

```console
$ git commit --amend
```

Or, if you want to combine the last three commits into one single commit:

```console
$ git reset --soft HEAD~3
$ git commit
```
If you want to merge all the commits in the branch into one commit you could run the following command:

```console
$ git reset --soft main
$ git commit
```

If you want complete freedom to rearrange and squash all your commits you can use interactive rebase:

```console
$ git rebase -i main
```

To push the changes to the server you use the following command:

```console
$ git push origin +<branch>
```

In our example:

```console
$ git push origin +users/kaufm/42_new-feature
```


![B17827_11_006](https://user-images.githubusercontent.com/5276337/146935884-2b9c6a11-3060-46ed-bb5b-8fc7bdce687a.png)












