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

![B17827_11_006](https://user-images.githubusercontent.com/5276337/146935884-2b9c6a11-3060-46ed-bb5b-8fc7bdce687a.png)
